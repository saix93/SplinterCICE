using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{
    [SerializeField]
    Transform interactionPoint;
    [SerializeField]
    Transform interactionHandler;
    [SerializeField]
    Animation interactionAnimation;
    [SerializeField]
    Collider interactionTarget;

    [SerializeField]
    ActivableObjectLinked activable;

    public System.Action onActivate;

    public Transform GetInteractionHandler()
    {
        return interactionHandler;
    }

    public Transform GetInteractionPoint()
    {
        return interactionPoint;
    }

    public Collider GetInteractionTarget()
    {
        return interactionTarget;
    }

    public IEnumerator InteractCoroutine()
    {
        if (interactionAnimation != null)
        {
            interactionAnimation.Play();
            yield return new WaitForSeconds(interactionAnimation.clip.length);
        }

        if (activable != null)
        {
            yield return StartCoroutine(activable.ActivateCoroutine());
        }

        if (onActivate != null)
        {
            // Son dos formas de hacer lo mismo:
            onActivate();
            // onActivate.Invoke();
        }
    }
}
