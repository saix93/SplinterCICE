using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    float movementSpeed = 1.5f;
    [SerializeField]
    float rotationSpeed = 360;

    [Header("References")]
    [SerializeField]
    Transform pathPointsRoot;

    NavMeshAgent agent;
    List<Transform> pathPoints;

    int currentPathPoint;
    bool followingPath;
    bool rotating = false;
    Vector3 targetRotation;
    Vector3 baseRotation;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (!HasPath())
        {
            followingPath = false;
            baseRotation = this.transform.forward;
        }
        else
        {
            followingPath = true;

            pathPoints = new List<Transform>();
            foreach (Transform child in pathPointsRoot)
            {
                pathPoints.Add(child);
            }

            currentPathPoint = 0;
            agent.SetDestination(pathPoints[currentPathPoint].position);
        }
    }

    private void Update()
    {
        if (followingPath)
        {
            if (!agent.pathPending && agent.remainingDistance < Mathf.Epsilon)
            {
                currentPathPoint = (currentPathPoint + 1) % pathPoints.Count;
                agent.SetDestination(pathPoints[currentPathPoint].position);
            }
        }

        if (rotating)
        {
            this.transform.forward = Vector3.RotateTowards(this.transform.forward, targetRotation, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0);

            if (Vector3.Angle(this.transform.forward, targetRotation) < Mathf.Epsilon)
            {
                rotating = false;
            }
        }
    }

    public void SetRotation(Vector3 targetForward)
    {
        rotating = true;
        targetRotation = targetForward;
    }

    public void StopMovement()
    {
        // Si estoy siguiendo una ruta...
        if (followingPath)
        {
            // Dejo de seguirla, y me detengo
            followingPath = false;
            agent.ResetPath();
        }

    }

    public void ResumeMovement()
    {
        if (HasPath() && !followingPath)
        {
            followingPath = true;
            agent.SetDestination(pathPoints[currentPathPoint].position);
        }
        else if (!HasPath())
        {
            SetRotation(baseRotation);
        }
    }

    public void SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }

    bool HasPath()
    {
        return pathPointsRoot != null;
    }
}
