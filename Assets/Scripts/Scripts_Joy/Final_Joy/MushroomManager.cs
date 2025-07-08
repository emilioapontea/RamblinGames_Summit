

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MushroomManager : MonoBehaviour
{
    // UI icons
    public Image powerIcon;
    public Image lavaIcon;
    public Image iceIcon;

    // Win panel
    public GameObject winPanel;

    public GameObject star; // Assign in inspector


    // Audio
    public AudioClip chord1;
    public AudioClip chord2;
    public AudioClip chord3;
    public AudioSource audioSource;

    // Internal state
    private bool powerCollected = false;
    private bool lavaCollected = false;
    private bool iceCollected = false;
    private int mushroomCollectedCount = 0;

    public void CollectMushroom(string type)
    {
        switch (type)
        {
            case "Power":
                if (!powerCollected)
                {
                    powerCollected = true;
                    powerIcon.color = Color.white;
                    OnMushroomCollected();
                }
                break;
            case "Lava":
                if (!lavaCollected)
                {
                    lavaCollected = true;
                    lavaIcon.color = Color.white;
                    OnMushroomCollected();
                }
                break;
            case "Ice":
                if (!iceCollected)
                {
                    iceCollected = true;
                    iceIcon.color = Color.white;
                    OnMushroomCollected();
                }
                break;
        }
    }

    private void OnMushroomCollected()
    {
        mushroomCollectedCount++;

        switch (mushroomCollectedCount)
        {
            case 1:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxMushroom1);  // assign this in AudioManager
                break;
            case 2:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxMushroom2);
                break;
            case 3:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxMushroom3);
                if (star != null)
                {
                    // star.ActivateStar(); // Make star visible after collecting all 3
                    star.GetComponent<StarController>().ActivateStar();
                }
                break;
        }
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxWin);
        Debug.Log("You Win!");
    }

    // UI Button Callbacks
    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NextLevel");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("QuitGame called - will quit in build.");
    }
}
