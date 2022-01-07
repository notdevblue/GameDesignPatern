using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    public class Heavy
    {
        private float health = 100.0f;
        private DummyData dData;

        public Heavy()
        {
            health = Random.Range(10.0f, 100.0f);
            dData = new DummyData();
        }
    }
}