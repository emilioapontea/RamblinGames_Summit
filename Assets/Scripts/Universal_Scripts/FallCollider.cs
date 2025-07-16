using UnityEngine;

public class FallCollider : MonoBehaviour
{
    public GameObject player; // Assign the player GameObject in the inspector
    [Header("Respawn Settings")]
    [Tooltip("Toggle to enable custom respawn behavior. If false, uses player's starting position")]
    public bool customRespawn = false; // Toggle for custom respawn behavior
    [Tooltip("Set the respawn point if custom respawn is enabled. Can be set in real time or in the inspector.")]
    public Vector3 respawnPoint; // Set the respawn point in the inspector
    private void Start()
    {
        if (customRespawn && respawnPoint == Vector3.zero)
        {
            Debug.LogWarning("Respawn point is not set. Please assign a respawn point in the inspector.");
        }
        if (!customRespawn)
        {
            respawnPoint = player.transform.position; // Default to player's current (starting) position
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Reset player position to respawn point
            player.transform.position = respawnPoint;

            // Optionally reset player state, health, etc.
            // Example: player.GetComponent<PlayerHealth>().ResetHealth();
        }
    }
}
