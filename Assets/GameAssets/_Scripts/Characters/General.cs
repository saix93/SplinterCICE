using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : SpecialEnemy
{
    [SerializeField] PlayerObjectiveIndicator objectiveIndicator;
    [SerializeField] PlayerCarEscape carScape;

    ObjectiveManager objectiveManager;

    private void Start()
    {
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }

    public override void Die()
    {
        base.Die();

        objectiveIndicator.SetNewObjective(carScape.HasKeys() ? objectiveManager.GetCar() : objectiveManager.GetCarKeys());
    }
}
