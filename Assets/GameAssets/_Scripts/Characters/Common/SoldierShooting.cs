using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierShooting : MonoBehaviour
{
    protected Animator animator;

    protected float aimingWeight;
    protected bool aiming;

    protected virtual void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    protected void UpdateAimingWeight()
    {
        aimingWeight = Mathf.MoveTowards(aimingWeight, aiming ? 1.0f : 0.0f, 4.0f * Time.deltaTime);

        animator.SetLayerWeight(1, aimingWeight);
    }

    public bool IsAiming()
    {
        return aiming;
    }
}
