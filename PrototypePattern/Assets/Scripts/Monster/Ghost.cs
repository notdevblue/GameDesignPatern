using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Ghost : Monster
    {
        private int hp;
        private int speed;

        static private int ghostCounter = 0;

        public Ghost(int hp, int speed)
        {
            this.hp = hp;
            this.speed = speed;

            ++ghostCounter;
        }

        public override Monster Clone()
        {
            return new Ghost(hp, speed);
        }

        public override void Talk()
        {
            Debug.Log(string.Format("i:{0}, h:{1}, s:{2}, ghost", ghostCounter, hp, speed));
        }
    }
}