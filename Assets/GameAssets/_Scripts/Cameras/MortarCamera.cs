using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MortarCamera : MonoBehaviour
{
    [SerializeField] Transform objectToFollow;

    CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = this.GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        cam.Follow = objectToFollow;
        cam.LookAt = objectToFollow;
    }
}
