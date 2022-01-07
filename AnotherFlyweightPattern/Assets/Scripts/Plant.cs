using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] public PlantDataSO plantInfo;

    private void Start()
    {
        plantInfo.SetRandomName();
        plantInfo.SetRandomThreat();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Player")) {
            if(plantInfo.Threat >= PlantDataSO.eThreat.Moderate) {
                PlayerController.dead = true;
            }
        }
    }
}
