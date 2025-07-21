using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class playerLose : MonoBehaviour
{
    public AudioSource deathPlayer;
    public GameObject deathImageObject;
    public float delayBeforeReload = 5f;
    [SerializeField] private bool playSoundEffects = true;
    // TODO: implement this in the hearts script instead
    private int ghostHitCount = 0;
    public int ghostLives = 3;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            ghostHitCount++;
            Debug.Log("Ghost hit! Current hit count: " + ghostHitCount);
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
            ghostHitCount++;
            Debug.Log("Ghost hit! Current hit count: " + ghostHitCount);
            if (ghostHitCount >= ghostLives)
            {
                if (playSoundEffects && deathPlayer != null) deathPlayer.Play();
                StartCoroutine(Death());
            }
        }
        if (other.CompareTag("Respawn"))
        {
            if (playSoundEffects && deathPlayer != null) deathPlayer.Play();
            StartCoroutine(Death());
        }

        if (other.CompareTag("Lava"))
        {
            if (GameStateManager.Instance.hasLavaMushroom)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxLava);
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
                StartCoroutine(Death());
            }
        }

        if (other.CompareTag("Water"))
        {
            if (GameStateManager.Instance.hasIceMushroom)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxIce);
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxDeath);
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Death()
    {
        // Pause the game
        Time.timeScale = 0f;

        if (deathImageObject != null)
            Debug.Log("Player has died, showing death image.");
            deathImageObject.SetActive(true); // show death image

        yield return new WaitForSecondsRealtime(delayBeforeReload);

        // Reset time and reload
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
