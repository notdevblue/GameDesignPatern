using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveObject : MonoBehaviour
    {
        private const float MOVE_STEP_DISTANCE = 1.0f;

        public void MoveFoward()
        {
            Move(Vector3.up);
        }
        public void MoveBackward()
        {
            Move(Vector3.down);
        }
        public void TurnLeft()
        {
            Move(Vector3.left);
        }
        public void TurnRight()
        {
            Move(Vector3.right);
        }

        private void Move(Vector3 dir)
        {
            transform.Translate(dir * MOVE_STEP_DISTANCE);
        }
    }
}