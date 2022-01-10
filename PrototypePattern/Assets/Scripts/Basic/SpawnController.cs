using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class SpawnController : MonoBehaviour
    {
        private Ghost ghostPrototype;
        private Daemon daemonPrototype;


        private Spawner[] monsterSpawners;

        private void Start()
        {
            ghostPrototype = new Ghost(10, 10);
            daemonPrototype = new Daemon(10, 10);

            monsterSpawners = new Spawner[]{
                new Spawner(ghostPrototype),
                new Spawner(daemonPrototype)
            };
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.D)) {
                monsterSpawners[1].Spawn().Talk();
            }
            if(Input.GetKeyDown(KeyCode.G)) {
                monsterSpawners[0].Spawn().Talk();
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                if((int)Time.time % 2 == 0) {
                    monsterSpawners[1].Spawn().Talk();
                } 
                else {
                    monsterSpawners[0].Spawn().Talk();
                }
            }

        }
        
    }
}