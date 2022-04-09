using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float SmoothTime = .25f;

    private static CameraFollow _instance;
    private Transform _target;
    private Vector3 _refVelocity;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        
        SmoothFollow();
    }

    /// <summary>
    /// Follows a target, with motion smoothing.
    /// </summary>
    private void SmoothFollow()
    {
        Vector3 newPosition = _target.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _refVelocity, SmoothTime);
    }

    public static void SetTarget(Transform transform)
    {
        _instance._target = transform;
    }
}
