using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Transform Transform => _instance.transform;
}
