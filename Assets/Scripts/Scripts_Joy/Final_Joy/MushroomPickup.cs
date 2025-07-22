// using UnityEngine;

// public class MushroomPickup : MonoBehaviour
// {
//     public enum MushroomType { Power, Lava, Ice }
//     public MushroomType mushroomType;

//     public AudioClip powerClip;
//     public AudioClip lavaClip;
//     public AudioClip iceClip;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             // Choose clip based on type
//             AudioClip clipToPlay = null;
//             string message = "";

//             switch (mushroomType)
//             {
//                 case MushroomType.Power:
//                     clipToPlay = powerClip;
//                     message = "Power Mushroom collected!";
//                     break;
//                 case MushroomType.Lava:
//                     clipToPlay = lavaClip;
//                     message = "Lava Mushroom collected!";
//                     break;
//                 case MushroomType.Ice:
//                     clipToPlay = iceClip;
//                     message = "Ice Mushroom collected!";
//                     break;
//             }

//             // Play sound at mushroom location
//             if (clipToPlay != null)
//             {
//                 AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
//             }

//             Debug.Log(message);
//             FindObjectOfType<MushroomManager>().CollectMushroom(mushroomType.ToString());

//             // Destroy the mushroom
//             Destroy(gameObject);
//         }
//     }

    
// }

using UnityEngine;

public class MushroomPickup : MonoBehaviour
{
    public GameStateManager gameStateManager;
    public enum MushroomType { Power, Lava, Ice }
    public MushroomType mushroomType;
    /// <summary>
    /// How fast should the mushrooms rotate about the y-axis.
    /// </summary>
    public float rotationSpeed = 0f;

    public AudioClip powerClip;
    public AudioClip lavaClip;
    public AudioClip iceClip;

    void FixedUpdate()
    {
        // Continuously rotate mushrooms about the y-axis
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Choose clip and message
            AudioClip clipToPlay = null;
            string message = "";

            switch (mushroomType)
            {
                case MushroomType.Power:
                    clipToPlay = powerClip;
                    message = "Power Mushroom collected!";
                    if (gameStateManager != null) gameStateManager.hasPowerMushroom = true;
                    break;
                case MushroomType.Lava:
                    clipToPlay = lavaClip;
                    message = "Lava Mushroom collected!";
                    if (gameStateManager != null) gameStateManager.hasLavaMushroom = true;
                    break;
                case MushroomType.Ice:
                    clipToPlay = iceClip;
                    message = "Ice Mushroom collected!";
                    if (gameStateManager != null) gameStateManager.hasIceMushroom = true;
                    break;
            }

            // Play sound at mushroom location
            if (clipToPlay != null)
            {
                AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
            }

            Debug.Log(message);

            // Notify the MushroomManager (UI + win logic)
            FindObjectOfType<MushroomManager>().CollectMushroom(mushroomType.ToString());

            // Destroy the mushroom
            Destroy(gameObject);
        }
    }
}
