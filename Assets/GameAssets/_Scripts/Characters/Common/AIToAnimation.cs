using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIToAnimation : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 velocity = agent.velocity;

        Vector3 localVelocity = this.transform.InverseTransformVector(velocity);

        anim.SetFloat("xSpeed", localVelocity.x);
        anim.SetFloat("zSpeed", localVelocity.z);
    }
}
