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

    private Transform _fireCtrl;

    void Start()
    {
        _fireCtrl = transform.Find("fire_ctrl");
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
                _fireCtrl.gameObject.SetActive(false);
                LogsLow.SetActive(false);
                LogsMedium.SetActive(false);
                LogsHigh.SetActive(false);
                break;
            case CampfireState.Low:
                _fireCtrl.gameObject.SetActive(true);
                _fireCtrl.localScale = new Vector3(1f, 1f, 1f);
                LogsLow.SetActive(true);
                LogsMedium.SetActive(false);
                LogsHigh.SetActive(false);
                break;
            case CampfireState.Medium:
                _fireCtrl.gameObject.SetActive(true);
                _fireCtrl.localScale = new Vector3(3f, 2f, 3f);
                LogsLow.SetActive(true);
                LogsMedium.SetActive(true);
                LogsHigh.SetActive(false);
                break;
            case CampfireState.High:
                _fireCtrl.localScale = new Vector3(4f, 2.5f, 4f);
                _fireCtrl.gameObject.SetActive(true);
                LogsLow.SetActive(true);
                LogsMedium.SetActive(true);
                LogsHigh.SetActive(true);
                break;
        }
    }
}
