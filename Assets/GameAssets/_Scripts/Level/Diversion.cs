using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Diversion : ActivableObjectLinked
{
    [SerializeField] Rigidbody[] diversionRigidbodies;
    [SerializeField] Transform firstEnemyPoint;
    [SerializeField] Transform secondEnemyPoint;
    [SerializeField] NavMeshAgent firstEnemyAgent;
    [SerializeField] NavMeshAgent secondEnemyAgent;

    private void Start()
    {
        ChangeKinematismOnRigidbodies(true);
    }

    void ChangeKinematismOnRigidbodies(bool newVal)
    {
        foreach (Rigidbody rb in diversionRigidbodies)
        {
            rb.isKinematic = newVal;
        }
    }

    public override IEnumerator ActivateCoroutine()
    {
        StartCoroutine(StartDiversion());

        yield return null;
    }

    IEnumerator StartDiversion()
    {
        WaitForSeconds wait = new WaitForSeconds(3);

        yield return wait;

        ChangeKinematismOnRigidbodies(false);

        firstEnemyAgent.SetDestination(firstEnemyPoint.position);
        secondEnemyAgent.SetDestination(secondEnemyPoint.position);
    }
}
