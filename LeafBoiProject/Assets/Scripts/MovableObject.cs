﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private bool _open = false;

    [SerializeField]
    protected float _smoothing;

    [SerializeField]
    protected Vector3 _initPos;

    [SerializeField]
    protected Vector3 _openPos;

    // Start is called before the first frame update
    private void Start()
    {
        _initPos = transform.position;
    }

    public void interactionHandler()
    {
        if (_open)
        {
            _open = false;
        }
        else
        {
            _open = true;
        }
    }

    private void FixedUpdate()
    {
        if (_open)
        {
            transform.position = Vector3.Lerp(transform.position, _initPos + _openPos, _smoothing);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, _initPos, _smoothing);
        }
    }
}