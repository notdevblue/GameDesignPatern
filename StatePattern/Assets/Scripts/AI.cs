using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent myAgent;
    Animator myAnim;

    public Transform playerTransform;

    State curState;

    private void Start()
    {
        myAgent = this.GetComponent<NavMeshAgent>();
        myAnim = this.GetComponent<Animator>();

        curState = new Idle(this.gameObject, myAgent, myAnim, playerTransform);
    }

    private void Update() {
        curState = this.curState.Process();
        Debug.Log(curState.stateName);
    }
}
