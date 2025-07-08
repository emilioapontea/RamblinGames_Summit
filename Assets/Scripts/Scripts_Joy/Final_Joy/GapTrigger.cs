using UnityEngine;

public class GapTrigger : MonoBehaviour
{
    public GameObject deathPanel;

    public AudioClip sfxFall;   // Falling sound
    public float fallDuration = 1.5f;  // Duration before death sound and panel

    private bool hasFallen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFallen)
        {
            hasFallen = true;
            Debug.Log("Player fell into the gap — Falling...");
            StartCoroutine(HandleFallAndDeath());
        }
    }

    private System.Collections.IEnumerator HandleFallAndDeath()
    {
        // Play falling sound
        AudioManager.Instance.PlaySFX(sfxFall);

        // Optional: Add a little downward force or animation here if needed

        // Wait before triggering death
        yield return new WaitForSecondsRealtime(fallDuration);

        // Show death UI and play death sound
        Debug.Log("Player died in the gap.");
        deathPanel.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
        Time.timeScale = 0f;
    }
}
