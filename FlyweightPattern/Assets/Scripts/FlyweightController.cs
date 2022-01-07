using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Flyweight
{
    public class FlyweightController : MonoBehaviour
    {
        private List<Heavy> heavyList = new List<Heavy>();
        private List<Flyweight> flyweightList = new List<Flyweight>();

        private void Start()
        {
            int numberOfObjs = 500000;

            // 데이터 공유 X Heavy object
            // for (int i = 0; i < numberOfObjs; ++i) {
            //     heavyList.Add(new Heavy());
            // }

            // Flyweight Pattern
            DummyData dData = new DummyData();
            for (int i = 0; i < numberOfObjs; ++i) {
                flyweightList.Add(new Flyweight(dData));
            }
        }
    }
}
