using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum eState // 가질 수 있는 상태 나열
    {
        IDLE, PARTROL, PURSUE, ATTACK, DEAD, RUNAWAY
    }

    public enum eEvent
    {
        ENTER, UPDATE, EXIT
    }

    public eState stateName;

    protected eEvent curEvent;

    protected GameObject myObj;
    protected NavMeshAgent myAgent;
    protected Animator myAnim;
    protected Transform playerTrm;

    protected State nextState;

    float detectDistance = 10.0f;
    float detectAngle = 30.0f;
    float shootDistance = 7.0f;

    public State(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTransform)
    {
        this.myObj = obj;
        this.myAgent = agent;
        this.myAnim = anim;
        this.playerTrm = targetTransform;

        this.curEvent = eEvent.ENTER;
    }

    public virtual void Enter()
    {
        curEvent = eEvent.UPDATE;
    }

    public virtual void Update()
    {
        curEvent = eEvent.UPDATE;
    }

    public virtual void Exit()
    {
        curEvent = eEvent.EXIT;
    }

    public State Process()
    {
        if(curEvent == eEvent.ENTER) Enter();
        if(curEvent == eEvent.UPDATE) Update();
        if (curEvent == eEvent.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        return (dir.magnitude < detectDistance && angle < detectAngle);
    }

    public bool CanAttackPlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        return (dir.magnitude < shootDistance);
    }

    public bool IsPlayerBehind()
    {
        Vector3 dir = myObj.transform.position - playerTrm.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        return (dir.magnitude < 3.0f && angle < detectAngle);
    }

}

public class Idle : State
{
    public Idle(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTransform) : base(obj, agent, anim, targetTransform)
    {
        stateName = eState.IDLE;
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if(CanSeePlayer()) {
            nextState = new Pursue(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if(Random.Range(0, 5000) < 10) {
            nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else {
            base.Update();
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Patrol : State
{
    private int index = -1;

    public Patrol(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTransform) : base(obj, agent, anim, targetTransform)
    {
        stateName = eState.PARTROL;
        myAgent.speed = 2.0f;
        myAgent.isStopped = false;
    }

    public override void Enter()
    {
        index = 0;
        myAnim.SetTrigger("isWalking");

        float mindist = float.MaxValue;

        for (int i = 0; i < GameEnvironment.Instance.checkpointList.Count; ++i) {
            if(mindist > Vector3.Distance(myObj.transform.position, GameEnvironment.Instance.checkpointList[i].transform.position)) {
                index = i;
            }
        }

        base.Enter();
    }

    public override void Update()
    {

        if (CanSeePlayer()) {
            nextState = new Pursue(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (IsPlayerBehind()) {
            nextState = new Runaway(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if(myAgent.remainingDistance < 1.0f) {
            if(index >= GameEnvironment.Instance.checkpointList.Count - 1) {
                index = 0;
            } else {
                ++index;
            }

            myAgent.SetDestination(GameEnvironment.Instance.checkpointList[index].transform.position);
            base.Update();
        }

        
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isWalking");
        base.Exit();
    }
}

public class Pursue : State
{
    public Pursue(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTransform) : base(obj, agent, anim, targetTransform)
    {
        stateName = eState.PURSUE;
        myAgent.speed = 5.0f;
        myAgent.isStopped = false;
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        // 추적
        myAgent.SetDestination(playerTrm.position);
        if(myAgent.hasPath) {
            if(CanAttackPlayer()) {
                nextState = new Attack(myObj, myAgent, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
            else if (!CanSeePlayer()) {
                nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
        }
        
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }

}

public class Attack : State
{
    float rotationSpeed = 2.0f;

    AudioSource shootEffect;

    public Attack(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTransform) : base(obj, agent, anim, targetTransform)
    {
        stateName = eState.ATTACK;
        shootEffect = obj.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isShooting");
        myAgent.isStopped = true;
        shootEffect.Play();

        base.Enter();
    }

    public override void Update()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        dir.y = 0;
        myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        
        if(!CanAttackPlayer()) {
            nextState = new Idle(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }

    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isShooting");
        base.Exit();
    }

}

public class Runaway : State
{
    GameObject safeObj;

    public Runaway(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTransform) : base(obj, agent, anim, targetTransform)
    {
        stateName = eState.RUNAWAY;
        safeObj = GameEnvironment.Instance.safePos.gameObject;
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isRunning");

        myAgent.speed = 7.0f;
        myAgent.isStopped = false;
        myAgent.SetDestination(safeObj.transform.position);

        base.Enter();
    }

    public override void Update()
    {
        if(myAgent.remainingDistance < 1.0f) {
            nextState = new Idle(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }

}