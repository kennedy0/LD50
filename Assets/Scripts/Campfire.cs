using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Campfire : MonoBehaviour
{
    private Actor _actor;

    public HexCell Cell => _actor.Cell;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
    }

    private void Update()
    {
        if (_actor.ActionReady)
        {
            _actor.Action<Pass>(Cell);
        }
    }
}
