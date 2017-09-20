using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    Animator animator;
    Rigidbody[] ragdollRigidbodies;

    bool ragdollActive;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        ragdollRigidbodies = this.transform.Find("Model").GetComponentsInChildren<Rigidbody>();

        SetRagdollActive(false);
    }

    public void SetRagdollActive(bool active)
    {
        animator.enabled = !active;

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !active;
        }
    }
}
