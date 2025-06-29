using UnityEngine;

public class CentralAudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // private void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    // {
    //     this.transform.position = position;
    //     audioSource.clip = clip;
    //     audioSource.spatialBlend = 1.0f; // Make the sound 3D
    //     audioSource.Play();
    // }

    // private void PlaySoundAtPosition(int clipIndex, Vector3 position)
    // {
    //     if (clipIndex < 0 || clipIndex >= clips.Length)
    //     {
    //         Debug.LogWarning("Clip index out of range");
    //         return;
    //     }
    //     PlaySoundAtPosition(clips[clipIndex], position);
    // }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySound(int clipIndex)
    {
        PlaySound(clips[clipIndex % clips.Length]);
    }
}
