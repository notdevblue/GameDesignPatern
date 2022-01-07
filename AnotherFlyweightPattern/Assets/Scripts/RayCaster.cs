using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
                PlantDataSO plant = raycastHit.transform.GetComponent<Plant>()?.plantInfo;
                if (plant == null) return;

                SetPlantInfo.SetPannel(plant.Icon, plant.Name, plant.Threat.ToString());
            }
        }
    }
}
