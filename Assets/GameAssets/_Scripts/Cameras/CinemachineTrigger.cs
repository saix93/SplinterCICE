using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] canvasTextArray;

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

            foreach (GameObject text in canvasTextArray)
            {
                text.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.gameObject.SetActive(false);

            foreach (GameObject text in canvasTextArray)
            {
                text.SetActive(false);
            }
        }
    }
}
