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

    [Header("Effects")]
    private ParticleSystem _embers;
    private ParticleSystem _emberBurst;

    void Start()
    {
        _fireCtrl = transform.Find("fire_ctrl");
        _embers = transform.Find("embers").Find("ember_particles").GetComponent<ParticleSystem>();
        _emberBurst = transform.Find("embers").Find("ember_burst_particles").GetComponent<ParticleSystem>();
        
        SetLevel(CampfireState.Low);
    }

    public void SetLevel(CampfireState state)
    {
        State = state;
        switch (State)
        {
            case CampfireState.Off:
                _fireCtrl.gameObject.SetActive(false);
                LogsLow.SetActive(false);
                LogsMedium.SetActive(false);
                LogsHigh.SetActive(false);
                break;
            case CampfireState.Low:
                _fireCtrl.gameObject.SetActive(true);
                _fireCtrl.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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
