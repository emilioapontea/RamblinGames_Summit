using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip pickupSound;
    private AudioSource audioSource;

    private bool isCollected = false;

    void Start()
    {
        CoinManager.Instance.RegisterGem();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isCollected || !other.CompareTag("Player")) return;

        isCollected = true;
        Debug.Log("Coin Collected");

        if (pickupSound)
        {
            Debug.Log("playing sound");
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        CoinManager.Instance.CollectGem();

        Destroy(gameObject);
    }
}
