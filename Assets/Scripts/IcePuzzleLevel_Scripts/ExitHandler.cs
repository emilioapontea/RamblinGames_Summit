using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHandler : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the next scene or perform any exit logic
            Debug.Log("Player has exited the level.");
            // Example: Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
