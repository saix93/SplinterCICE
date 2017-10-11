using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Docks", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
