using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// The name of the scene to load upon calling LoadScene().
    /// </summary>
    public string destinationScene = "Title";

    public void LoadScene()
    {
        // Resume game if paused
        Time.timeScale = 1f;
        SceneManager.LoadScene(destinationScene);
    }
}
