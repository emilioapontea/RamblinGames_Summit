using UnityEngine;

public class HintManager : MonoBehaviour
{
    public GameObject hintPanel; // Assign your hint UI panel here
    public float displayTime = 5f;

    void Start()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(true);
            Invoke(nameof(HideHint), displayTime);
        }
    }

    void HideHint()
    {
        hintPanel.SetActive(false);
    }
}
