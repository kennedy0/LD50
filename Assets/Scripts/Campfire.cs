using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Campfire : MonoBehaviour
{
    public int Fuel = 50;
    
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
            Burn();
        }
    }

    /// <summary>
    /// Burn fuel.
    /// </summary>
    private void Burn()
    {
        Fuel -= 1;
        _actor.Action<Pass>(Cell);
    }

    /// <summary>
    /// Spend wood to add fuel to the fire.
    /// </summary>
    public void Kindle(int wood)
    {
        Fuel += wood * 10;
    }
}
