using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "planData", menuName = "Plant Data")]
public class PlantDataSO : ScriptableObject
{
    public enum eThreat : int
    {
        None = 0,
        Low,
        Moderate,
        High
    }

    [SerializeField] private string plantName;
    [SerializeField] private eThreat plantThreat;
    [SerializeField] private Texture icon;

    public string Name { get { return plantName; } }
    public eThreat Threat { get { return plantThreat; } }
    public Texture Icon { get { return icon; } }

    public void SetRandomName()
    {
        plantName = Utils.RandomName(Random.Range(4, 10));
    }

    public void SetRandomThreat()
    {
        plantThreat = Utils.RandomThreat<eThreat>();
    }


}
