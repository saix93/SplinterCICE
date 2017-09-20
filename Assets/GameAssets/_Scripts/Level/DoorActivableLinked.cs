using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DoorActivableLinked : ActivableObjectLinked
{
    CinemachineBrain camBrain;
    CinemachineVirtualCamera myCamera;
    Animator animator;

    private void Awake()
    {
        camBrain = Camera.main.GetComponent<CinemachineBrain>();
        myCamera = this.GetComponentInChildren<CinemachineVirtualCamera>(includeInactive: true);
        animator = this.GetComponent<Animator>();
    }

    public override IEnumerator ActivateCoroutine()
    {
        WaitForSeconds waitCamera = new WaitForSeconds(camBrain.m_DefaultBlend.m_Time);

        myCamera.gameObject.SetActive(true);

        yield return waitCamera;

        animator.SetBool("Open", !animator.GetBool("Open"));

        yield return new WaitForSeconds(1);

        myCamera.gameObject.SetActive(false);

        yield return waitCamera;
    }
}
