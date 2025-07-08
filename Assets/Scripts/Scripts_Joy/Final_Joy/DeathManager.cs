using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public GameObject deathPanel;

    void Start()
    {
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    void Update()
    {
        // Press K to simulate death (for testing)
        if (Input.GetKeyDown(KeyCode.K))
        {
            TriggerDeath();
        }
    }

    public void TriggerDeath()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
            Time.timeScale = 0f; // pause game
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("QuitGame called - will quit in build.");
    }
}
