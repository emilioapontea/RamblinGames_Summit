using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 8;
    public Vector3 respawnPosition;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        respawnPosition = transform.position; // Set this at level start, or set via Inspector
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            DieAndRespawn();
        }
    }

    void DieAndRespawn()
    {
        // Optional: play death animation/sound here
        currentHealth = maxHealth;
        transform.position = respawnPosition;
        // Optionally reset velocity, etc.
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}