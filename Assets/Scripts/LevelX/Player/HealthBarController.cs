using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats.Instance is null!");
            return;
        }

        int maxHearts = Mathf.CeilToInt(PlayerStats.Instance.MaxTotalHealth);

        heartContainers = new GameObject[maxHearts];
        heartFills = new Image[maxHearts];

        PlayerStats.Instance.onHealthChangedCallback += UpdateHeartsHUD;

        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats.Instance is null during UpdateHeartsHUD!");
            return;
        }

        Debug.Log($"Updating hearts HUD. Health: {PlayerStats.Instance.Health}, Max: {PlayerStats.Instance.MaxHealth}");

        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            bool shouldBeActive = i < PlayerStats.Instance.MaxHealth;
            heartContainers[i].SetActive(shouldBeActive);
            Debug.Log($"Heart {i}: Active = {shouldBeActive}");
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < PlayerStats.Instance.Health)
            {
                heartFills[i].fillAmount = 1f;
                Debug.Log($"Heart {i}: Full");
            }
            else
            {
                heartFills[i].fillAmount = 0f;
                Debug.Log($"Heart {i}: Empty");
            }
        }

        if (PlayerStats.Instance.Health % 1f != 0)
        {
            int partialIndex = Mathf.FloorToInt(PlayerStats.Instance.Health);
            float partialAmount = PlayerStats.Instance.Health % 1f;

            if (partialIndex >= 0 && partialIndex < heartFills.Length)
            {
                heartFills[partialIndex].fillAmount = partialAmount;
                Debug.Log($"Heart {partialIndex}: Partial fill = {partialAmount}");
            }
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < PlayerStats.Instance.MaxTotalHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);

            heartContainers[i] = temp;

            Transform fill = temp.transform.Find("HeartFill");
            if (fill == null)
            {
                Debug.LogError($"HeartFill child not found in HeartContainer prefab at index {i}");
                continue;
            }

            Image fillImage = fill.GetComponent<Image>();
            if (fillImage == null)
            {
                Debug.LogError($"HeartFill at index {i} does not have an Image component!");
                continue;
            }

            heartFills[i] = fillImage;

            Debug.Log($"Instantiated Heart {i} under {heartsParent.name}, position: {temp.transform.position}");
        }
    }
}
