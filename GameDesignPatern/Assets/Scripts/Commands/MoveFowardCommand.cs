using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveFowardCommand : Command
    {
        private MoveObject moveObject;

        public MoveFowardCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }

        public override void Execute()
        {
            moveObject.MoveFoward();
        }

        public override void Undo()
        {
            moveObject.MoveBackward();
        }
    }
}