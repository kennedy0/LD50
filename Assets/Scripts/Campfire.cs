using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    private Actor _actor;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
    }

    private void Update()
    {
        if (!_actor.Ready)
        {
            return;
        }
        
        _actor.Pass();
    }
}
