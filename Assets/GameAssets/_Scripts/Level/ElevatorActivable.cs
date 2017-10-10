using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorActivable : ActivableObjectLinked
{
    [Header("References")]
    [SerializeField] GameObject firstLight;
    [SerializeField] GameObject secondLight;
    [SerializeField] PlayerObjectiveIndicator objectiveIndicator;
    [SerializeField] Transform newObjective;

    [Header("Variables")]
    [SerializeField][Range(0, 5)] float lightSwapTime = 1;

    Animator animator;

    bool alreadyCalled = false;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public override IEnumerator ActivateCoroutine()
    {
        if (!alreadyCalled)
        {
            animator.SetTrigger("Activate");

            yield return new WaitForSeconds(1);

            StartCoroutine(ControlLights());
            objectiveIndicator.SetNewObjective(newObjective);

            alreadyCalled = true;
        }
    }

    IEnumerator ControlLights()
    {
        bool aux = true;
        WaitForSeconds wait = new WaitForSeconds(lightSwapTime);

        float duration = Time.time + 12;

        while(Time.time < duration)
        {
            firstLight.SetActive(aux);
            secondLight.SetActive(!aux);

            yield return wait;

            aux = !aux;
        }

        firstLight.SetActive(false);
        secondLight.SetActive(false);
    }

    public IEnumerator EndLevel()
    {
        StartCoroutine(ControlLights());

        yield return new WaitForSeconds(8);

        SceneManager.LoadScene("MillitaryBase");
    }
}
