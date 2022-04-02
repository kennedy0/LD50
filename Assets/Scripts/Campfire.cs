using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CampfireState
{
    Off,
    Low,
    Medium,
    High,
}

public class Campfire : MonoBehaviour
{
    [Header("General")]
    public CampfireState State;
    
    [Header("Model")]
    public GameObject LogsLow;
    public GameObject LogsMedium;
    public GameObject LogsHigh;

    void Start()
    {
        State = CampfireState.Medium;
        transform.position = Utilities.GridToWorldPosition(0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetLevel(CampfireState.Off);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetLevel(CampfireState.Low);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetLevel(CampfireState.Medium);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetLevel(CampfireState.High);
        }
    }

    public void SetLevel(CampfireState state)
    {
        switch (state)
        {
            case CampfireState.Off:
                LogsLow.SetActive(false);
                LogsMedium.SetActive(false);
                LogsHigh.SetActive(false);
                break;
            case CampfireState.Low:
                LogsLow.SetActive(true);
                LogsMedium.SetActive(false);
                LogsHigh.SetActive(false);
                break;
            case CampfireState.Medium:
                LogsLow.SetActive(true);
                LogsMedium.SetActive(true);
                LogsHigh.SetActive(false);
                break;
            case CampfireState.High:
                LogsLow.SetActive(true);
                LogsMedium.SetActive(true);
                LogsHigh.SetActive(true);
                break;
        }
    }
}
