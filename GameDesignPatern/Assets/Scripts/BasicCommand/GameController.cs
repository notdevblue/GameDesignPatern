using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class GameController : MonoBehaviour
    {
        public MoveObject moveObj;

        private Command buttonW;
        private Command buttonA;
        private Command buttonS;
        private Command buttonD;

        private List<Command> recordList = new List<Command>();
        private int currentRecordIndex;
        private bool moveable = true;

        private void Start()
        {
            buttonW = new MoveFowardCommand(moveObj);
            buttonS = new MoveBackCommand(moveObj);
            buttonA = new TurnLeftCommand(moveObj);
            buttonD = new TurnRightCommand(moveObj);
        }

        private void Update()
        {
            if(!moveable) return;

            if(Input.GetKeyDown(KeyCode.W)) {
                ExecuteNewCommand(buttonW);
                AddHistory(buttonW);
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                ExecuteNewCommand(buttonA);
                AddHistory(buttonA);
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                ExecuteNewCommand(buttonS);
                AddHistory(buttonS);
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                ExecuteNewCommand(buttonD);
                AddHistory(buttonD);
            }

            // 키매핑: 
            if(Input.GetKeyDown(KeyCode.Space)) {
                SwapKeys(ref buttonA, ref buttonD);
            }

            // Undo:
            if(Input.GetKeyDown(KeyCode.U)) { // 넴
                if(currentRecordIndex < 0) return;
                UndoCommand(recordList[currentRecordIndex--]); 
                Debug.Log("Undo");
            }

            // Redo:
            if(Input.GetKeyDown(KeyCode.R)) { // 넴
                if(currentRecordIndex + 1 >= recordList.Count) return;
                ExecuteNewCommand(recordList[currentRecordIndex++]);
                Debug.Log("Redo");
            }
            
            // Replay:
            if(Input.GetKeyDown(KeyCode.Return)) {
                StartCoroutine(Replay());
            }
        }

        private void ExecuteNewCommand(Command commandButton)
        {
            commandButton.Execute();
            Debug.Log("입력");
        }

        private void UndoCommand(Command commandButton)
        {
            commandButton.Undo();
        }

        private void AddHistory(Command commandButton)
        {
            RemoveUnusedHistory();

            recordList.Add(commandButton);
            currentRecordIndex = recordList.Count - 1;
        }

        IEnumerator Replay()
        {
            moveObj.transform.position = Vector3.zero;
            RemoveUnusedHistory();
            moveable = false;


            for (int i = 0; i < recordList.Count; ++i)
            {
                yield return new WaitForSeconds(0.5f);
                ExecuteNewCommand(recordList[i]);
                Debug.Log("Replay");
            }

            moveable = true;
        }

        private void RemoveUnusedHistory()
        {
            if (currentRecordIndex < recordList.Count - 1) {
                recordList.RemoveRange(currentRecordIndex + 1, recordList.Count - (currentRecordIndex + 1));
            }
        }

        private void SwapKeys(ref Command key1, ref Command key2)
        {
                Command temp = null;
                temp = key1;
                key1 = key2;
                key2 = temp;
                Debug.Log("스왑");
        }
    }
}