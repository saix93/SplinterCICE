using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    [SerializeField]
    Transform ikTarget;
    [SerializeField]
    float transitionDuration = 0.5f;

    Animator anim;

    bool ikActive;
    float ikWeight;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        ikWeight = Mathf.MoveTowards(ikWeight, ikActive ? 1 : 0, Time.deltaTime / transitionDuration);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (ikTarget)
        {
            anim.SetLookAtPosition(ikTarget.position);
            anim.SetLookAtWeight(ikWeight);

            UpdateIK(AvatarIKGoal.LeftHand);
        }
    }

    private void UpdateIK(AvatarIKGoal goal)
    {
        anim.SetIKPosition(goal, ikTarget.position);
        anim.SetIKPositionWeight(goal, ikWeight);

        anim.SetIKRotation(goal, ikTarget.rotation);
        anim.SetIKRotationWeight(goal, ikWeight);
    }

    public void ActivateIK(Transform target)
    {
        ikActive = true;
        ikTarget = target;
    }

    public void DeactivateIK()
    {
        ikActive = false;
    }

    public float GetTransitionDuration()
    {
        return transitionDuration;
    }
}
