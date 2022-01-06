using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveBackCommand : Command
    {
        private MoveObject moveObject;

        public MoveBackCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }

        public override void Execute()
        {
            moveObject.MoveBackward();
        }

        public override void Undo()
        {
            moveObject.MoveFoward();
        }
    }
}