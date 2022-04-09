using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    
    private Actor _actor;

    private void Awake()
    {
        _instance = this;
        _actor = GetComponent<Actor>();
    }

    public static Transform Transform => _instance.transform;

    public void Update()
    {
        if (!_actor.Ready)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _actor.Move(_actor.Cell.NorthWest);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _actor.Move(_actor.Cell.North);
        }
        if (Input.GetKey(KeyCode.E))
        {
            _actor.Move(_actor.Cell.NorthEast);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _actor.Move(_actor.Cell.SouthWest);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _actor.Move(_actor.Cell.South);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _actor.Move(_actor.Cell.SouthEast);
        }
    }
}
