﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElevatorTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] ElevatorActivable elevatorActivable;
    [SerializeField] AudioSource audioElevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Activate");
            StartCoroutine(elevatorActivable.EndLevel());

            audioElevator.Play();

            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
