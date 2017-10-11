using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscapeTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Player player;
    [SerializeField] GameObject timeline;

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
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.GetComponent<NavMeshAgent>().enabled = false;
            timeline.SetActive(true);
        }
    }
}
