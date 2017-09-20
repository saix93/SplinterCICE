using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTrigger : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = this.GetComponentInChildren<CinemachineVirtualCamera>(includeInactive: true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.gameObject.SetActive(false);
        }
    }
}
