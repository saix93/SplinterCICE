using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertCircle : MonoBehaviour
{
    [SerializeField]
    float minRadio = .5f;
    [SerializeField]
    float speedFactor = 1f;

    NavMeshAgent agent;
    Projector projector;
    SphereCollider ownCollider;

    private void Awake()
    {
        agent = this.GetComponentInParent<NavMeshAgent>();
        projector = this.GetComponent<Projector>();
        ownCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        projector.orthographicSize = Mathf.Max(minRadio, agent.velocity.magnitude * speedFactor);
        ownCollider.radius = projector.orthographicSize;
    }
}
