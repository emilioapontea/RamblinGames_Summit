using UnityEngine;

public class DrawbridgeAudioPlayer : MonoBehaviour
{
    /// <summary>
    /// Should this object be able to play sound effects?
    /// </summary>
    [SerializeField] private bool playSoundEffects = true;
    private AudioSource bridgeAudioSource;

    public void Awake()
    {
        bridgeAudioSource = GetComponent<AudioSource>();
        if (bridgeAudioSource == null)
        {
            Debug.LogError("Drawbridge Audio Player could not find an audio source component.");
        }
    }

    public void PlayBridgeSound()
    {
        if (playSoundEffects && !bridgeAudioSource.isPlaying)
        {
            bridgeAudioSource.Play();
        }
    }
}
