using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Spawner
    {
        private Monster prototype;

        public Spawner(Monster monster)
        {
            this.prototype = monster;
        }

        public Monster Spawn()
        {
            return prototype.Clone();
        }
    }
}