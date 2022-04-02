using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    [Header("Model")]
    public GameObject LogsLow;
    public GameObject LogsMedium;
    public GameObject LogsHigh;

    void Start()
    {
        transform.position = Utilities.GridToWorldPosition(0, 0);
    }

    void Update()
    {
        
    }
}
