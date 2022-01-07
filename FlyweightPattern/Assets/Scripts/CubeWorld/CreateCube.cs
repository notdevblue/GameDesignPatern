using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    public int width;
    public int depth;

    public GameObject cubePrefab;


    void Start()
    {
        for (int x = 0; x < width; ++x) {
            for (int z = 0; z < depth; ++z) {
                // Vector3 pos = new Vector3(x, 0, z);

                Vector3 pos = new Vector3(x, Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 3.0f, z);

                GameObject obj = Instantiate(cubePrefab, pos, Quaternion.identity);
            }
        }
    }
}
