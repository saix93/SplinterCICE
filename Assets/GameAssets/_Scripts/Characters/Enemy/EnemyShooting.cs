using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : SoldierShooting
{
    [SerializeField]
    float _alertTime = 4;

    Soldier player;
    Enemy enemy;
    EnemyMovement enemyMovement;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.Find("Player").GetComponent<Soldier>();
        enemy = this.GetComponent<Enemy>();
        enemyMovement = this.GetComponent<EnemyMovement>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (enemy.CanSeeTarget(player))
        {
            if (!aiming)
            {
                aiming = true;
                enemyMovement.StopMovement();
                enemy.SetState(Enemy.State.Attack);
                StartCoroutine(ShootPlayerCoroutine());
            }
        }
        UpdateAimingWeight();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Player"))
        {
            ActivateAlert();

            enemyMovement.SetRotation(player.transform.position - this.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Player"))
        {
            StartCoroutine(CancelAlertCoroutine());
        }
    }

    private void ActivateAlert()
    {
        if (enemy.GetState() == Enemy.State.Idle)
        {
            enemy.SetState(Enemy.State.Alert);
            enemyMovement.StopMovement();
        }
        else if (enemy.GetState() == Enemy.State.Alert)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator ShootPlayerCoroutine()
    {
        enemyMovement.SetRotation(player.transform.position - this.transform.position);
        yield return new WaitForSeconds(0.5f);
        player.Die();
    }

    IEnumerator CancelAlertCoroutine()
    {
        yield return new WaitForSeconds(_alertTime);
        enemy.SetState(Enemy.State.Idle);
        enemyMovement.ResumeMovement();
    }
}
