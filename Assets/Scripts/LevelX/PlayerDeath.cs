using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathImageObject; 
    public float delayBeforeReload = 5f;

    private void Start()
    {
        if (deathImageObject != null)
            deathImageObject.SetActive(false);
    }

    // Called by both OnTriggerEnter and PlayerStats
    public void HandleDeath()
    {
        Debug.Log("Player died. Showing death UI.");
        StartCoroutine(ShowImageAndReload());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleDeath();
        }
    }

    private IEnumerator ShowImageAndReload()
    {
        if (deathImageObject != null)
            deathImageObject.SetActive(true); // make it visible

        yield return new WaitForSeconds(delayBeforeReload);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
