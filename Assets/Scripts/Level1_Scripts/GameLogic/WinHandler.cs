using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    public CanvasGroup winCanvas;
    public AudioSource musicPlayer;
    public AudioSource winSoundEffect;
    private bool winState = false;
    [SerializeField] private string nextScene;
    void Start()
    {
        if (winCanvas == null) Debug.LogError("WinHandler does not have a canvas group.");
        if (nextScene == null) Debug.LogError("WinHandler does not have a destination scene set.");
        if (musicPlayer == null) Debug.LogError("Winhandler does not have a reference to the music player.");
        if (winSoundEffect == null) Debug.LogError("Winhandler does not have a reference to a win sound effect player.");
    }

    void Update()
    {
        if (winState && Input.GetKey(KeyCode.Return))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextScene);
        }
    }

    public void WinLevel()
    {
        PauseMenuToggle.allowUnpause = false; // Disable unpausing through pause menu
        musicPlayer.Stop();
        winSoundEffect.Play();
        winCanvas.alpha = 1f;
        winCanvas.interactable = true;
        winCanvas.blocksRaycasts = true;
        winState = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            WinLevel();
        }
    }
}
