using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM Clips")]
    // public AudioClip bgmMain;
    public AudioClip bgmPause;
    public AudioClip bgmWin;
    public AudioClip bgmDeath;

    [Header("SFX Clips")]
    public AudioClip sfxMushroom1;
    public AudioClip sfxMushroom2;
    public AudioClip sfxMushroom3;
    public AudioClip sfxWin;
    public AudioClip sfxDeath;
    public AudioClip sfxPauseOn;
    public AudioClip sfxPauseOff;
    public AudioClip sfxLava;
    public AudioClip sfxIce;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // ====== BGM ======
    // public void PlayBGM(AudioClip clip, bool loop = true)
    // {
    //     bgmSource.clip = clip;
    //     bgmSource.loop = loop;
    //     bgmSource.Play();
    // }

    // public void StopBGM()
    // {
    //     bgmSource.Stop();
    // }

    // ====== SFX ======
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
