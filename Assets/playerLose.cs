using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class playerLose : MonoBehaviour
{
    public AudioSource deathPlayer;
    public GameObject deathImageObject;
    public GameStateManager gameStateManager;
    public float delayBeforeReload = 5f;
    [SerializeField] private bool playSoundEffects = true;
    // TODO: implement this in the hearts script instead
    private int ghostHitCount = 0;
    public int ghostLives = 3;
    private bool isInvincible = false;
    public float invincibilityDuration = 1f;
 
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            if (isInvincible) return;
            isInvincible = true;
            StartCoroutine(ResetInvincibility());
            ghostHitCount++;
            PlayerStats.Instance.TakeDamage(1);
            // Debug.Log("Skeleton hit! Current hit count: " + ghostHitCount);
            if (ghostHitCount >= ghostLives)
            {
                if (playSoundEffects && deathPlayer != null) deathPlayer.Play();
                StartCoroutine(Death());

            }

        }

    }
 


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            if (isInvincible) return;
            isInvincible = true;
            StartCoroutine(ResetInvincibility());
            ghostHitCount++;
            //take damage
            PlayerStats.Instance.TakeDamage(1);
            //Debug.Log("Ghost hit! Current hit count: " + ghostHitCount);
            if (ghostHitCount >= ghostLives)
            {
                if (playSoundEffects && deathPlayer != null) deathPlayer.Play();
                StartCoroutine(Death());
            }
        }
        if (other.CompareTag("Respawn"))
        {
            
            StartCoroutine(Death());
        }

        if (other.CompareTag("Lava"))
        {
            if (gameStateManager != null && gameStateManager.hasLavaMushroom)
            {
                // if (AudioManager.Instance!= null && AudioManager.Instance.sfxLava != null)
                //     AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxLava);
                
            }
            else
            {
                // if (AudioManager.Instance != null && AudioManager.Instance.sfxDeath != null)
                //     AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
                StartCoroutine(Death());
            }
        }

        if (other.CompareTag("Water"))
        {
            if (gameStateManager != null && gameStateManager.hasIceMushroom)
            {
                // if (AudioManager.Instance != null && AudioManager.Instance.sfxIce != null)
                //     AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxIce);
            }
            else
            {
                // if (AudioManager.Instance != null && AudioManager.Instance.sfxDeath != null)
                //     AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
                
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Death()
    {
        // Pause the game
        Time.timeScale = 0f;

        if (deathImageObject != null)
            //Debug.Log("Player has died, showing death image.");
        deathImageObject.SetActive(true); // show death image
        if (playSoundEffects && deathPlayer != null)
        {
            deathPlayer.Play();
        }
        yield return new WaitForSecondsRealtime(delayBeforeReload);

        // Reset time and reload
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(0.5f); // tweak as needed
        isInvincible = false;
    }
}
