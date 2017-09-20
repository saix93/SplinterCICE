using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float crouchingSpeed = 2;
    [SerializeField]
    float walkSpeed = 4;
    [SerializeField]
    float crouchingSprintSpeed = 4;
    [SerializeField]
    float sprintSpeed = 8;

    NavMeshAgent agent;
    Animator anim;
    PlayerInteraction playerInteraction;
    PlayerShooting playerShooting;

    bool rotating;
    Vector3 rotationTarget;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        playerInteraction = this.GetComponent<PlayerInteraction>();
        playerShooting = this.GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        MovePlayer();
        Crouch();
        Sprint();

        Rotate();
    }

    private bool IsCrouching()
    {
        return anim.GetBool("crouching");
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButton(0) && !playerInteraction.IsInteracting() && !playerShooting.IsAiming())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // int layerMask = 1 << LayerMask.NameToLayer("Walkable");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                SetDestination(hit.point);
            }
        }
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C) && !playerInteraction.IsInteracting())
        {
            bool shouldCrouch = !anim.GetBool("crouching");
            anim.SetBool("crouching", shouldCrouch);

            agent.speed = shouldCrouch ? crouchingSpeed : walkSpeed;
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !playerInteraction.IsInteracting() && !playerShooting.IsAiming())
        {
            agent.speed = IsCrouching() ? crouchingSprintSpeed : sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !playerInteraction.IsInteracting() && !playerShooting.IsAiming())
        {
            agent.speed = IsCrouching() ? crouchingSpeed : walkSpeed;
        }
    }

    private void Rotate()
    {
        if (rotating)
        {
            this.transform.forward = Vector3.RotateTowards(this.transform.forward, rotationTarget, agent.angularSpeed * Mathf.Deg2Rad * Time.deltaTime, 0);

            if (Vector3.Angle(this.transform.forward, rotationTarget) < Mathf.Epsilon)
            {
                rotating = false;
            }
        }
    }

    public void ResetToDefaultMovement()
    {
        agent.speed = walkSpeed;
        anim.SetBool("crouching", false);
    }

    public void SetDestination(Vector3 destination, bool updateRotation = true)
    {
        agent.updateRotation = updateRotation;
        agent.SetDestination(destination);
    }

    public void SetRotation(Vector3 targetForward)
    {
        rotating = true;
        rotationTarget = targetForward;
    }

    public bool IsMovingToDestination()
    {
        return agent.pathPending || agent.remainingDistance > Mathf.Epsilon; // Mathf.Epsilon --> Un float muy pequeño
    }

    public bool IsRotating()
    {
        return rotating;
    }

    public void StopMoving()
    {
        agent.ResetPath();
    }
}
