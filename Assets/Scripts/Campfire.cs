using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Campfire : MonoBehaviour
{
    public int Fuel = 50;
    
    private Actor _actor;
    private TextMeshProUGUI _text;

    public HexCell Cell => _actor.Cell;

    private void Awake()
    {
        _text = GameObject.Find("Wood Counter").GetComponent<TextMeshProUGUI>();
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
        _text.SetText("0");
        Fuel += wood * 10;
    }
}
