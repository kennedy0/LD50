using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embers : MonoBehaviour
{
    private ParticleSystem _burstParticles;

    private void Awake()
    {
        _burstParticles = transform.Find("ember_burst_particles").GetComponent<ParticleSystem>();
    }

    public void PlayBurst()
    {
        _burstParticles.Emit(50);
    }
}
