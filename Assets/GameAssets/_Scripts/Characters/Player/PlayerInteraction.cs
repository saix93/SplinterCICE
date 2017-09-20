using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    Transform interactRaycastPoint;
    [SerializeField]
    float interactionDistance = 2;
    [SerializeField]
    float playerViewCone = 50;
    [SerializeField]
    GameObject interactUI;

    IKControl ikControl;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;

    bool interacting;

    public bool IsInteracting()
    {
        return interacting;
    }

    private void Awake()
    {
        ikControl = this.GetComponent<IKControl>();
        playerMovement = this.GetComponent<PlayerMovement>();
        playerShooting = this.GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        if (!interacting)
        {
            InteractiveButton possibleInteraction = CheckPossibleInteraction();

            if (Input.GetKeyDown(KeyCode.E) && possibleInteraction)
            {
                // Interactúa con el objeto más cercano
                StartCoroutine(InteractWithNearestObject(possibleInteraction));
            }
        }

        Debug.DrawRay(interactRaycastPoint.position, interactRaycastPoint.forward * interactionDistance, Color.blue, .5f);
    }

    InteractiveButton CheckPossibleInteraction()
    {
        if (!playerShooting.IsAiming())
        {
            // Por cada objeto interactivo cercano
            Collider[] nearestColliders = Physics.OverlapSphere(interactRaycastPoint.position, interactionDistance);

            for (int i = 0; i < nearestColliders.Length; i++)
            {
                Collider col = nearestColliders[i];

                // Comprueba si es interactivo
                if (col.CompareTag("Interactable"))
                {
                    Debug.DrawRay(interactRaycastPoint.position, col.gameObject.transform.position - interactRaycastPoint.position, Color.red, .5f);
                    InteractiveButton interactiveButton = col.GetComponent<InteractiveButton>();
                    
                    // Comprueba si hay línea de visión con el objeto
                    if (CanInteractWithTarget(interactiveButton.GetInteractionTarget()))
                    {
                        // Actualiza la UI y devuelve el objeto interactivo
                        interactUI.SetActive(true);
                        return interactiveButton;
                    }
                }
            }
        }

        interactUI.SetActive(false);
        return null;
    }

    bool CanInteractWithTarget(Collider target)
    {
        // Calcula el angulo entre el forward del personaje y la dirección hacia el objeto interactivo
        Vector3 forward = Vector3.ProjectOnPlane(this.transform.forward, Vector3.up);
        Vector3 directionToTarget = Vector3.ProjectOnPlane(target.transform.position - this.transform.position, Vector3.up);

        float angleToTarget = Vector3.Angle(forward, directionToTarget);

        if (angleToTarget < playerViewCone)
        {
            // Si el ángulo es válido, se comprueba si existe línea de visión hacia el objeto
            RaycastHit hitInfo;
            if (Physics.Raycast(interactRaycastPoint.position, target.transform.position - interactRaycastPoint.position, out hitInfo))
            {
                if (hitInfo.collider == target)
                {
                    // Si el objetivo golpeado por el raycast es el esperado, se devuelve true
                    return true;
                }
            }
        }

        return false;
    }

    IEnumerator InteractWithNearestObject(InteractiveButton interactiveButton)
    {
        interacting = true;
        interactUI.SetActive(false);

        // Coloca el personaje en la posición para interactuar con el objeto
        playerMovement.ResetToDefaultMovement();
        playerMovement.SetDestination(interactiveButton.GetInteractionPoint().position, updateRotation:false);
        playerMovement.SetRotation(interactiveButton.GetInteractionPoint().forward);

        yield return new WaitWhile(() => playerMovement.IsMovingToDestination() && playerMovement.IsRotating());

        // Activa el IK para pulsar el botón
        ikControl.ActivateIK(interactiveButton.GetInteractionHandler());

        WaitForSeconds waitTransitionDuration = new WaitForSeconds(ikControl.GetTransitionDuration());

        yield return waitTransitionDuration;

        yield return StartCoroutine(interactiveButton.InteractCoroutine());

        // Desactiva el IK cuando termina la animación del botón
        ikControl.DeactivateIK();

        yield return waitTransitionDuration;

        interacting = false;
    }
}
