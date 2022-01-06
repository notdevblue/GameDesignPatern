using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject playerObj;
    private Animator anim;

    private Command keyQ;
    private Command keyW;
    private Command keyE;
    private Command upArrow;

    List<Command> reverseList = new List<Command>();
    private int idx = 0;
    private bool moveable = true;

    private void Start()
    {
        anim = playerObj.GetComponent<Animator>();
        keyQ = new PerformJump();
        keyW = new PerformKick();
        keyE = new PerformPunch();
        upArrow = new MoveFoward();

        Camera.main.GetComponent<CameraFollow360>().player = playerObj.transform;
    }
    
    private void Update()
    {
        if(!moveable) return;
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            keyQ.Execute(anim);
            AddStack(keyQ);
        } 
        else if (Input.GetKeyDown(KeyCode.W)) {
            keyW.Execute(anim);
            AddStack(keyW);
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            keyE.Execute(anim);
            AddStack(keyE);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            upArrow.Execute(anim);
            AddStack(upArrow);
        } 

        if(Input.GetKeyDown(KeyCode.Return)) {
            StartCoroutine(Reverse());
        }

        if(Input.GetKeyDown(KeyCode.U)) {
            Undo();
        }
    }

    private void AddStack(Command command)
    {
        RemoveUnusedHistory();

        reverseList.Add(command);
        idx = reverseList.Count - 1;
    }

    private bool Undo()
    {
        if (idx < 0) {
            idx = 0;
            return false;
        }

        reverseList[idx--].Execute(anim, true);
        return true;
    }

    private void RemoveUnusedHistory() {
        if (idx < reverseList.Count - 1)
        {
            reverseList.RemoveRange(idx + 1, reverseList.Count - (idx + 1));
        }
    }

    IEnumerator Reverse()
    {
        moveable = false;
        RemoveUnusedHistory();

        while(Undo()) {
            yield return new WaitForSeconds(1.0f);
        }

        moveable = true;
    }


}
