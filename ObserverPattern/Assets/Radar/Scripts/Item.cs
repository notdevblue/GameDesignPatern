using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public EventSO dropped;
    public EventSO picked;
    public Image icon;

    private void Start()
    {
        dropped.Occured(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Player")) {
            picked.Occured(this.gameObject);
        }
    }
}
