using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectiveIndicator : MonoBehaviour
{
    [SerializeField] Transform arrow;
    [SerializeField] Transform objective;

    private void Update()
    {
        Vector3 direction = (objective.position - this.transform.position).normalized;

        arrow.position = this.transform.position + direction * 1;
        arrow.LookAt(objective.position);
    }

    public void SetNewObjective(Transform newObjective)
    {
        objective = newObjective;
    }
}