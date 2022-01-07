using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlantInfo : MonoBehaviour
{
    static private SetPlantInfo inst = null;

    private void Awake() {
        inst = this;
    }

    public GameObject plantInfoPanel;
    public GameObject plantIcon;
    public Text planeName;
    public Text phreatLevel;

    static public void OpenPlantPanel()
    {
        inst.plantInfoPanel.SetActive(true);
    }

    static public void SetPannel(Texture icon, string name, string level)
    {
        inst.plantIcon.GetComponent<RawImage>().texture = icon;
        inst.planeName.text = name;
        inst.phreatLevel.text = level;
        OpenPlantPanel();
    }

    public void ClosePlantPanel()
    {
        plantInfoPanel.SetActive(false);
    }
}
