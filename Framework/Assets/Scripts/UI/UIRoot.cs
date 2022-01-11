using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIRoot : MonoBehaviour
{
    public Image blackPannel;
    private CanvasScaler rootCvs;
    private float nowRatio;

    public bool isPaused = false;

    private void Awake()
    {
        GAmeSceneClass.globalUIRoot = this;
        
        rootCvs = this.gameObject.GetComponent<CanvasScaler>();

        Global.referenceResolution = new Vector2();
        Global.referenceResolution = rootCvs.referenceResolution;
        Global.blackPannel = blackPannel;
        nowRatio = Convert.ToSingle((double)Screen.height / (double)Screen.width);
        Debug.LogFormat("해상도{0}:{1}, 비율{2:F6}, dpi{3}", Screen.height, Screen.width, nowRatio, Screen.dpi);

        rootCvs.referenceResolution = Global.referenceResolution;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            // backbutton
        }
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        isPaused = focusStatus;

        if(!isPaused) {
            // Save Data.
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus) {
            // Save data.
        }
        else {
            Screen.fullScreen = true;
        }
    }

    private void OnApplicationQuit()
    {
        // Save Data.
    }
}
