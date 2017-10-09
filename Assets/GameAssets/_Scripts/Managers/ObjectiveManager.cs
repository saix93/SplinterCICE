using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] Transform generalObjective;
    [SerializeField] Transform carKeyObjective;
    [SerializeField] Transform carObjective;

    public Transform GetGeneral()
    {
        return generalObjective;
    }

    public Transform GetCarKeys()
    {
        return carKeyObjective;
    }

    public Transform GetCar()
    {
        return carObjective;
    }
}
