using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action OnDead;
    public float disableTime = 3.0f;
    
    private void Awake()     => OnDead += () => { };
    private void OnEnable()  => Invoke(nameof(Disable), disableTime);
    private void Disable()   => this.gameObject.SetActive(false);
    private void OnDisable() => OnDead();
    
}
