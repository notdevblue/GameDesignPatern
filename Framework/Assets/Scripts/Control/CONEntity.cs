using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONEntity : MonoBehaviour
{
    public ePrefabs myKind;

    protected GameObject myObj;

    public Transform myTrm;

    public Vector3 myVelocity;

    protected bool bFirstUpdate;


    public virtual void Awake()
    {
        myTrm = this.transform;
        myObj = this.gameObject;
    }

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {

    }

    public virtual void Start()
    {

    }

    public void SetActive(bool bValue)
    {
        myObj.SetActive(bValue);

        if(!bValue) {
            myTrm.position = Vector3.zero;
            // myTrm.SetParent(GAmeSceneClass.gMGPool.myTrm, false);

            // if(GAmeSceneClass.gMGGame == null) return;
            
        }
    }
}
