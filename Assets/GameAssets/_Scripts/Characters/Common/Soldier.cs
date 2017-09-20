using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    float visionAngle;
    [SerializeField]
    float visionDistance;
    [SerializeField]
    Transform visionRaycastOriginPoint;
    [SerializeField]
    Transform visionRaycastTargetPoint;
    [SerializeField]
    LayerMask visionRaycastLayerMask;

    RagdollControl ragdollControl;
    Collider ownCollider;
    NavMeshAgent agent;

    public float GetVisionDistance()
    {
        return visionDistance;
    }

    public float GetVisionAngle()
    {
        return visionAngle;
    }

    protected virtual void Awake()
    {
        ragdollControl = this.GetComponent<RagdollControl>();
        ownCollider = this.GetComponent<Collider>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    public virtual void Die()
    {
        ragdollControl.SetRagdollActive(true);
        ownCollider.enabled = false;
        agent.enabled = false;
    }

    public bool CanSeeTarget(Soldier soldier)
    {
        // Comprueba el ángulo de visión del personaje
        Vector3 forward = this.transform.forward;
        Vector3 directionToTarget = soldier.transform.position - this.transform.position;
        float angle = Vector3.Angle(forward, directionToTarget);
        if (angle < visionAngle / 2)
        {
            // Lanza un rayo para comprobar la línea de visión y la distancia máxima hacia el objetivo
            Vector3 raycastOrigin = visionRaycastOriginPoint.position;
            Vector3 raycastTarget = soldier.visionRaycastTargetPoint.position;
            Vector3 raycastDirection = raycastTarget - raycastOrigin;
            Debug.DrawRay(raycastOrigin, raycastDirection * visionDistance);

            RaycastHit hitInfo;
            if (Physics.Raycast(raycastOrigin, raycastDirection, out hitInfo, visionDistance, visionRaycastLayerMask))
            {
                Collider hitCollider = hitInfo.collider;
                if (hitCollider.GetComponent<Soldier>() == soldier)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
