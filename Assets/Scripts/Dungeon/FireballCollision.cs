using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            // AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxGhostHit);
        }
    }
}
