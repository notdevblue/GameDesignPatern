using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    public GameObject cubePrefab;
    private int killedEnemyCount = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(cubePrefab, Vector3.zero, Quaternion.identity).GetComponent<Cube>().OnDead += () => { Debug.Log($"{++killedEnemyCount} 명의 적을 죽였습니다. 점수: {killedEnemyCount}");  };
        }
    }
}
