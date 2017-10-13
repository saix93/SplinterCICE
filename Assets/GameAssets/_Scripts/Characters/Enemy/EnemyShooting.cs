using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : SoldierShooting
{
    [SerializeField] float alertTime = 4;
    [SerializeField] float pursueTime = 5;
    [SerializeField] AudioSource audioShoot;

    Soldier player;
    Enemy enemy;
    EnemyMovement enemyMovement;

    Vector3 currentPosition;
    Vector3 noisePosition;

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
            noisePosition = player.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Player"))
        {
            enemy.SetState(Enemy.State.Pursue);
            StartCoroutine(StartPursueCoroutine());
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
        audioShoot.Play();
        player.Die();
    }

    IEnumerator StartPursueCoroutine()
    {
        yield return new WaitForSeconds(alertTime);
        currentPosition = this.transform.position;
        enemyMovement.SetDestination(noisePosition);

        yield return new WaitForSeconds(pursueTime);
        enemyMovement.SetDestination(currentPosition);

        yield return new WaitForSeconds(pursueTime);
        enemy.SetState(Enemy.State.Idle);
        enemyMovement.ResumeMovement();
    }
}
