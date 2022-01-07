using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    public class Flyweight : MonoBehaviour
    {
        private float health = 100.0f;
        private DummyData dData;

        public Flyweight(DummyData dData)
        {
            health = Random.Range(10.0f, 100.0f);
            this.dData = dData;
        }
    }
}