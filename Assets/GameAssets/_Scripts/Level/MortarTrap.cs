using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MortarTrap : ActivableObjectLinked
{
    [SerializeField] Transform usableMortarAmmo;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] GameObject interaction;
    [SerializeField] GameObject canvas;
    [SerializeField] AudioSource audioExplosion;
    [SerializeField] AudioSource audioMortar;

    Animator usableMortarAmmoAnim;

    private void Start()
    {
        usableMortarAmmoAnim = usableMortarAmmo.GetComponent<Animator>();
    }

    public override IEnumerator ActivateCoroutine()
    {
        audioMortar.Play();

        WaitForSeconds wait2Seconds = new WaitForSeconds(3);
        cam.gameObject.SetActive(true);

        yield return wait2Seconds;

        usableMortarAmmoAnim.SetTrigger("Activate");

        yield return new WaitForSeconds(3);

        Collider[] afectedByMortar = Physics.OverlapSphere(usableMortarAmmo.position, 5);

        foreach (Collider col in afectedByMortar)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                enemy.Die();
            }
        }

        GameObject explosion = Instantiate(_explosionPrefab, usableMortarAmmo.position, usableMortarAmmo.rotation);

        audioExplosion.Play();
        Destroy(explosion, 5);
        Destroy(usableMortarAmmo.gameObject);

        yield return wait2Seconds;

        cam.gameObject.SetActive(false);

        interaction.SetActive(false);
        canvas.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (usableMortarAmmo)
        {
            Gizmos.DrawSphere(usableMortarAmmo.position, 5);
        }
    }
}
