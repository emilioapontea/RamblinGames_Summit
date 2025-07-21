using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class playerLose : MonoBehaviour
{
    public AudioSource deathPlayer;
    public GameObject deathImageObject; 
    public float delayBeforeReload = 5f;
    [SerializeField] private bool playSoundEffects = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            if (playSoundEffects && deathPlayer != null)
                deathPlayer.Play();

            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        // Pause the game
        Time.timeScale = 0f;

        if (deathImageObject != null)
            deathImageObject.SetActive(true); // show death image

        yield return new WaitForSecondsRealtime(delayBeforeReload);

        // Reset time and reload
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
