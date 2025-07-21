using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditsCanvas; // assign in inspector

    private bool isShowing = false;

    public void ShowCredits()
    {
        if (creditsCanvas != null)
        {
            creditsCanvas.SetActive(true);
            isShowing = true;
        }
    }

    void Update()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.Return)) // or KeyCode.KeypadEnter
        {
            creditsCanvas.SetActive(false);
            isShowing = false;
        }
    }
}
