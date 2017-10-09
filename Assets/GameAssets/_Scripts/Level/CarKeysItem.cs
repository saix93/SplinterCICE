using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKeysItem : MonoBehaviour
{
    [SerializeField] Transform floatingObject;

    [SerializeField] float _amplitude = .005f;
    [SerializeField] float _frecuency = 1f;

    Player player;
    ObjectiveManager objectiveManager;

    private void Start()
    {
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Vector3 position = floatingObject.position;
        position.y += Mathf.Sin(Time.time * _frecuency) * _amplitude;

        floatingObject.position = position;
        floatingObject.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerCarEscape>().PickUpCarKeys();

            if (player.GetObjectiveIndicator().GetCurrentObjective() != objectiveManager.GetGeneral())
            {
                player.GetObjectiveIndicator().SetNewObjective(objectiveManager.GetCar());
            }

            Destroy(this.gameObject);
        }
    }
}
