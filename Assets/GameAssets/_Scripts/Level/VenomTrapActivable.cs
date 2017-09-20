using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VenomTrapActivable : ActivableObjectLinked
{
    ParticleSystem particles;
    CinemachineVirtualCamera cam;

    private void Awake()
    {
        particles = this.GetComponent<ParticleSystem>();
        cam = this.GetComponentInChildren<CinemachineVirtualCamera>(includeInactive: true);
        particles.Stop();
    }

    public override IEnumerator ActivateCoroutine()
    {
        cam.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        particles.Play();
        yield return new WaitForSeconds(3);
        particles.Stop();
        cam.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
    }
}
