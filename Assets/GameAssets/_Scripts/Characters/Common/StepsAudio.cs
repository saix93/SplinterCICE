using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StepsAudio : MonoBehaviour
{
    NavMeshAgent agent;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void AnimationStep()
    {
        if (agent.velocity.magnitude > 2.5f)
        {
            // Debug.Log("Paso correr");
        }
    }

    void AnimationStepWalk()
    {
        if (agent.velocity.magnitude < 2.5f)
        {
            // Debug.Log("Paso caminar");
        }
    }
}
