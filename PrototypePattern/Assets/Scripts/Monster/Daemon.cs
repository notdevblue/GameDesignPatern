using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Daemon : Monster
    {
        private int hp;
        private int speed;

        static private int daemonCounter = 0;

        public Daemon(int hp, int speed)
        {
            this.hp = hp;
            this.speed = speed;

            ++daemonCounter;
        }

        public override Monster Clone()
        {
            return new Daemon(hp, speed);
        }

        public override void Talk()
        {
            Debug.Log(string.Format("Failed to start dhcpcd. id:{0}", daemonCounter));
        }
    }
}