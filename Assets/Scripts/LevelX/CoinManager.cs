using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Required for TextMeshProUGUI

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int totalGems;
    public int collectedGems;

    public TextMeshProUGUI gemText;  // 👈 Assign in Inspector

    public GameObject doorLeft;
    public GameObject doorRight;
    public Vector3 doorLeftOpenOffset = new Vector3(0, 6f, 0);
    public Vector3 doorRightOpenOffset = new Vector3(0, 6f, 0);
    public float doorOpenSpeed = 2f;

    private bool doorsOpening = false;
    private Vector3 doorLeftTarget;
    private Vector3 doorRightTarget;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        UpdateGemText();  // Initial update
    }

    public void RegisterGem()
    {
        totalGems++;
        UpdateGemText();
    }

    public void CollectGem()
    {
        collectedGems++;
        UpdateGemText();

        /*  
        //Changed so guard opens doors instead of it opening automatically.
        if (collectedGems >= totalGems)
        {
            Debug.Log("All gems collected!");
            OpenDoors();
        }
        */
    }

    void UpdateGemText()
    {
        if (gemText != null)
            gemText.text = $"Gems: {collectedGems} / {totalGems}";
    }

    public void OpenDoors()
    {
        doorLeftTarget = doorLeft.transform.position + doorLeftOpenOffset;
        doorRightTarget = doorRight.transform.position + doorRightOpenOffset;
        doorsOpening = true;
    }

    void Update()
    {
        if (doorsOpening)
        {
            doorLeft.transform.position = Vector3.MoveTowards(
                doorLeft.transform.position,
                doorLeftTarget,
                doorOpenSpeed * Time.deltaTime
            );

            doorRight.transform.position = Vector3.MoveTowards(
                doorRight.transform.position,
                doorRightTarget,
                doorOpenSpeed * Time.deltaTime
            );

            if (Vector3.Distance(doorLeft.transform.position, doorLeftTarget) < 0.01f &&
                Vector3.Distance(doorRight.transform.position, doorRightTarget) < 0.01f)
            {
                doorsOpening = false;
            }
        }
    }
}
