using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{
    public EventSound3D eventSound3D;
    public AudioClip unlockedSecretClip;
    // public AudioClip doorOpenClip;
    private UnityAction allPuzzlesSolvedEventListener;
    // private UnityAction<Vector3> doorOpenEventListener;

    private void Awake()
    {
        allPuzzlesSolvedEventListener = new UnityAction(allPuzzlesSolvedEventHandler);
        // doorOpenEventListener = new UnityAction<Vector3>(doorOpenEventHandler);
    }

    private void OnEnable()
    {
        EventManager.StartListening<AllPuzzlesSolvedEvent>(allPuzzlesSolvedEventListener);
        // EventManager.StartListening<DoorOpenEvent, Vector3>(doorOpenEventListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening<AllPuzzlesSolvedEvent>(allPuzzlesSolvedEventListener);
        // EventManager.StopListening<DoorOpenEvent, Vector3>(doorOpenEventListener);

    }

    private void allPuzzlesSolvedEventHandler()
    {
        EventSound3D newEventSound = Instantiate(eventSound3D, Random.insideUnitSphere * 10f, Quaternion.identity);
        newEventSound.audioSrc.clip = unlockedSecretClip;
        newEventSound.audioSrc.volume = 1f;
        newEventSound.audioSrc.minDistance = 1f;
        newEventSound.audioSrc.maxDistance = 500f;

        newEventSound.audioSrc.Play();
        // Destroy(newEventSound.gameObject, unlockedSecretClip.length); // Clean up after sound finishes
    }

    // private void doorOpenEventHandler(Vector3 position)
    // {
    //     EventSound3D newEventSound = Instantiate(eventSound3D, position, Quaternion.identity);
    //     newEventSound.audioSrc.clip = doorOpenClip;
    //     newEventSound.audioSrc.Play();
    //     Destroy(newEventSound.gameObject, doorOpenClip.length); // Clean up after sound finishes
    // }
}