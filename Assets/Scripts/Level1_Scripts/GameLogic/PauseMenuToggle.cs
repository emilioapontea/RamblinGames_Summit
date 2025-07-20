using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    /// <summary>
    /// Whether the game should unpause upon closing the pause menu.
    /// Disable this upon level completion or player death
    /// </summary>
    public static bool allowUnpause = true;
    [Header("Sound Effects")]
    /// <summary>
    /// Should sound effects be played when pausing and unpausing?
    /// </summary>
    [SerializeField] private bool playPauseSoundEffects = true;
    /// <summary>
    /// Audio Source that plays a sound effect upon pausing the game.
    /// </summary>
    public AudioSource pauseSoundEffect;
    /// <summary>
    /// Audio Source that plays a sound effect upon unpausing the game.
    /// </summary>
    public AudioSource unpauseSoundEffect;
    [Header("Music")]
    /// <summary>
    /// Reference to the audio source playing the scene's music.
    /// </summary>
    // public AudioSource musicSource;
    /// <summary>
    /// Should the music track pause when the game is paused?
    /// </summary>
    public bool stopMusicOnPause = true;

    void Awake()
    {
        allowUnpause = true; // Allow unpausing upon scene load
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("GameManager: Could not find CanvasGroup component in UI Canvas.");
        }
        // if (musicSource == null)
        // {
        //     Debug.LogError("GameManager: Music Audio Source reference is missing.");
        // }
        if (playPauseSoundEffects)
        {
            if (pauseSoundEffect == null)
            {
                Debug.LogError("GameManager: Could not find reference to pause sound effect.");
            }
            if (unpauseSoundEffect == null)
            {
                Debug.LogError("GameManager: Could not find reference to unpause sound effect.");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvasGroup.interactable)
            {
                // Disable and hide the canvas if it is currently enabled
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;

                // Unpause the music (if previously paused)
                if (stopMusicOnPause && allowUnpause && BackgroundAudioManager.Instance != null) BackgroundAudioManager.Instance.Play();

                // Play the unpause sound effect (if enabled)
                if (playPauseSoundEffects) unpauseSoundEffect.Play();

                // Unpause the game upon disabling the canvas
                if (allowUnpause) Time.timeScale = 1f;
            }
            else
            {
                // Enable and show the canvas if it is currently disabled
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;

                // Pause the music
                if (stopMusicOnPause && BackgroundAudioManager.Instance != null) BackgroundAudioManager.Instance.Pause();

                // Play the pause sound effect (if enabled)
                if (playPauseSoundEffects) pauseSoundEffect.Play();

                // Pause the game upon enabling the canvas
                Time.timeScale = 0f;
            }
        }
    }
}
