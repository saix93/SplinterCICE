using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StepsAudio : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSteps;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void AnimationStep()
    {
        if (agent.velocity.magnitude > 2.5f)
        {
            CreateStepAndPlay();
        }
    }

    void AnimationStepWalk()
    {
        if (agent.velocity.magnitude <= 2.5f)
        {
            CreateStepAndPlay();
        }
    }

    void CreateStepAndPlay()
    {
        int index = Random.Range(0, audioSteps.Length);
        AudioSource step = Instantiate(audioSteps[index], this.transform.position, this.transform.rotation);
        step.Play();
        Destroy(step.gameObject, 1);
    }
}
