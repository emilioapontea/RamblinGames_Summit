using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    /// <summary>
    /// The name of the scene to load upon starting game.
    /// </summary>
    public string startingScene = "Intro";

    public void StartGame()
    {
        // Resume game if paused
        Time.timeScale = 1f;
        SceneManager.LoadScene(startingScene);
    }
}
