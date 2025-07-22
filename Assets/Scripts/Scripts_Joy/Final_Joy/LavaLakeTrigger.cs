// // using UnityEngine;

// // public class LavalakeTrigger : MonoBehaviour
// // {
// //     private void OnTriggerEnter(Collider other)
// //     {
// //         if (other.CompareTag("Player"))
// //         {
// //             Debug.Log("Entered the lava");
// //         }
// //     }

// //     private void OnTriggerExit(Collider other)
// //     {
// //         if (other.CompareTag("Player"))
// //         {
// //             Debug.Log("Exited the lava");
// //         }
// //     }
// // }

// using UnityEngine;

// public class LavalakeTrigger : MonoBehaviour
// {
//     public AudioClip lavaSound;      // Assign your lava sound clip in Inspector
//     private AudioSource audioSource;

//     private void Start()
//     {
//         // Get or add AudioSource component
//         audioSource = GetComponent<AudioSource>();
//         if (audioSource == null)
//             audioSource = gameObject.AddComponent<AudioSource>();

//         audioSource.playOnAwake = false;
//         audioSource.clip = lavaSound;
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Entered the lava");
//             audioSource.Play();
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Exited the lava");
//         }
//     }
// }
// using UnityEngine;

// public class LavalakeTrigger : MonoBehaviour
// {
//     public GameObject deathPanel;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {

//             if (!GameStateManager.Instance.hasLavaMushroom)
//             {
//                 Debug.Log("Died in Lava!");
//                 deathPanel.SetActive(true); // Show death panel
//                 Time.timeScale = 0f;
//                 AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
//             }
//             else
//             {
//                 Debug.Log("Safe in Lava (Lava Mushroom collected)");
//                 AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxLava);
//             }
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Exited the lava");
//             // lavaAudioSource.Stop();
//         }
//     }
// }
