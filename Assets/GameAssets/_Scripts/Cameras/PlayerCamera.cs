using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Vector3 distanceToPlayer;
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        distanceToPlayer = this.transform.position - player.position;
    }

    private void LateUpdate()
    {
        this.transform.position = player.position + distanceToPlayer;
    }
}
