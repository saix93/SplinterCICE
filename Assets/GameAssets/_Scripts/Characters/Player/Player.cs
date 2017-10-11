using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Soldier
{
    [SerializeField] GameObject alertArea;
    [SerializeField] PlayerObjectiveIndicator playerObjectiveIndicator;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    PlayerInteraction playerInteraction;

    protected override void Awake()
    {
        base.Awake();
        playerMovement = this.GetComponent<PlayerMovement>();
        playerShooting = this.GetComponent<PlayerShooting>();
        playerInteraction = this.GetComponent<PlayerInteraction>();
    }

    public override void Die()
    {
        base.Die();

        alertArea.SetActive(false);
        playerMovement.enabled = false;
        playerShooting.enabled = false;
        playerInteraction.enabled = false;

        Invoke("LoadMainMenu", 3);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public PlayerObjectiveIndicator GetObjectiveIndicator()
    {
        return playerObjectiveIndicator;
    }
}
