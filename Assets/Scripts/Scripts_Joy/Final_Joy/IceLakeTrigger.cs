// using UnityEngine;

// public class IcelakeTrigger : MonoBehaviour
// {
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Entered the ice");
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Exited the ice");
//         }
//     }
// }

// using UnityEngine;

// public class IcelakeTrigger : MonoBehaviour
// {
//     public AudioClip iceSound;       // Assign your ice sound clip in Inspector
//     private AudioSource audioSource;

//     private void Start()
//     {
//         // Get or add AudioSource component
//         audioSource = GetComponent<AudioSource>();
//         if (audioSource == null)
//             audioSource = gameObject.AddComponent<AudioSource>();

//         audioSource.playOnAwake = false;
//         audioSource.clip = iceSound;
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Entered the ice");
//             audioSource.Play();
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Exited the ice");
//         }
//     }
// }


using UnityEngine;

public class IcelakeTrigger : MonoBehaviour
{
    public GameObject deathPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            

            if (!GameStateManager.Instance.hasIceMushroom)
            {
                Debug.Log("Died in Ice!");
                deathPanel.SetActive(true); // Show death panel
                Time.timeScale = 0f;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
            }
            else
            {
                Debug.Log("Safe in Ice (Ice Mushroom collected)");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxIce);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exited the ice");
            // lavaAudioSource.Stop();
        }
    }
}
