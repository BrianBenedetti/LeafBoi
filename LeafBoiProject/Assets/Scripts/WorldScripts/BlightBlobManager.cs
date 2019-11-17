﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlightBlobManager : MonoBehaviour
{
    public float delay;
    public GameObject BlightParticleEffect;
    Vector3 pos;

    void Start()
    {
        pos = this.gameObject.transform.position;
    }

    public void pickUp()
    {
        StartCoroutine(interactHandle());
    }

    private IEnumerator interactHandle()
    {
        //INSERT THE CODE FOR THE PARTICLE SYSTEM TO TAKE EFFECT HERE
        yield return new WaitForSeconds(delay);
        
        Destroy(this.gameObject);
    }
}