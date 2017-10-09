using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKeysHolder : SpecialEnemy
{
    [SerializeField] GameObject carKeysPrefab;

    public override void Die()
    {
        base.Die();

        Instantiate(carKeysPrefab, this.transform.position, new Quaternion());
    }
}
