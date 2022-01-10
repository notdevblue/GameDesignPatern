using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProCubeSpawner : MonoBehaviour
{
    void Update()
    {
        if(Random.Range(0, 100) < 10) {
            // ProcCube.CreateCube(Vector3.zero);
            ProcCube.Clone(Vector3.zero);
        }
    }
}
