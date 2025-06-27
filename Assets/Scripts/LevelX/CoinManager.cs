using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    private int totalCoins = 0;
    private int collectedCoins = 0;

    public TextMeshProUGUI coinText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateCoinUI();
    }

    public void RegisterCoin()
    {
        totalCoins++;
        UpdateCoinUI();
    }

    public void CollectCoin(int value)
    {
        collectedCoins += value;
        UpdateCoinUI();

        if (collectedCoins >= totalCoins)
        {
            Debug.Log("All coins collected!");
            // doorToOpen.OpenDoor(); // Optional
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {collectedCoins} / {totalCoins}";
        }
    }
}
