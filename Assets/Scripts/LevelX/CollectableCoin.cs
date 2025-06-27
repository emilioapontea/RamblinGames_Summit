using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip collectSound;

    private bool isCollected = false;

    void Start()
    {
        CoinManager.Instance.RegisterCoin();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isCollected || !other.CompareTag("Player")) return;

        isCollected = true;
        Debug.Log("Coin Collected");

        if (collectSound)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        CoinManager.Instance.CollectCoin(coinValue);

        Destroy(gameObject);
    }
}
