using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour
{
    public static BackgroundAudioManager Instance;

    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern to persist this object
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
