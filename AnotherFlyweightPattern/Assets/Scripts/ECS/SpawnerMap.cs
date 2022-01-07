using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMap : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject cubePrefab;

    private void Start() {
        for (int x = 0; x < width; ++x) {
            for (int z = 0; z < height; ++z) {
                Vector3 pos = new Vector3(x, Mathf.PerlinNoise(x * 0.21f, z * 0.21f), z);
                GameObject obj = Instantiate(cubePrefab, pos, Quaternion.identity);
            }
        }
    }
}
