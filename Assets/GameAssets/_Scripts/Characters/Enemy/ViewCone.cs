using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ViewCone : MonoBehaviour
{
    [SerializeField] Color idleColor = Color.green;
    [SerializeField] Color alertColor = Color.yellow;
    [SerializeField] Color attackColor = Color.red;

    Enemy enemy;
    Projector projector;

    private void Awake()
    {
        projector = this.GetComponent<Projector>();
        enemy = this.GetComponentInParent<Enemy>();
    }

    private void Start()
    {
        projector.material = new Material(projector.material);
    }

    private void Update()
    {
        switch (enemy.GetState())
        {
            case Enemy.State.Idle:
                projector.material.color = idleColor;
                break;
            case Enemy.State.Alert:
                projector.material.color = alertColor;
                break;
            case Enemy.State.Attack:
                projector.material.color = attackColor;
                break;
        }

        projector.orthographicSize = enemy.GetVisionDistance();
        projector.material.SetFloat("_Angle", enemy.GetVisionAngle() / 2);
    }
}
