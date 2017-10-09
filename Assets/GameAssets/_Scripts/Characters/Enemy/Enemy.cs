using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Soldier
{
    public enum State
    {
        Idle,
        Alert,
        Attack
    }

    [SerializeField]
    GameObject viewCone;

    EnemyMovement enemyMovement;
    EnemyShooting enemyShooting;

    State state;

    public State GetState()
    {
        return state;
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    protected override void Awake()
    {
        base.Awake();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyShooting = GetComponent<EnemyShooting>();
    }

    private void Start()
    {
        SetState(State.Idle);
    }

    public override void Die()
    {
        base.Die();

        SpecialEnemy specialEnemy = this.GetComponent<SpecialEnemy>();

        if (specialEnemy != null)
        {
            specialEnemy.Die();
        }

        viewCone.SetActive(false);
        enemyMovement.enabled = false;
        enemyShooting.enabled = false;
    }
}
