using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScapeTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Player player;

    ObjectiveManager objectiveManager;

    private void Start()
    {
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.GetComponent<PlayerCarEscape>().HasKeys() && player.GetObjectiveIndicator().GetCurrentObjective() == objectiveManager.GetCar())
        {
            // Activar cinemática de salida
            print("FIN DEL JUEGO");
        }
    }
}
