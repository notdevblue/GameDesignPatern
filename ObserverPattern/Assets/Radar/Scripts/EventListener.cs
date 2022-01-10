using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject> { }


public class EventListener : MonoBehaviour
{
    public EventSO gEvent;

    public UnityGameObjectEvent responseObj = new UnityGameObjectEvent();

    public void OnEnable()
    {
        gEvent.Register(this);
    }

    public void OnDisable()
    {
        gEvent.UnRegister(this);
    }

    public void OnEventOccurs(GameObject obj)
    {
        responseObj.Invoke(obj);
    }

}
