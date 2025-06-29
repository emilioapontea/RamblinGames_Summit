using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    /// <summary>
    /// Whether the game should unpause upon closing the pause menu.
    /// Disable this upon level completion or player death
    /// </summary>
    public static bool allowUnpause = true;

    void Awake()
    {
        allowUnpause = true; // Allow unpausing upon scene load
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("Could not find CanvasGroup component in UI Canvas.");
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvasGroup.interactable)
            {
                // Disable and hide the canvas if it is currently enabled
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;

                // Unpause the game upon disabling the canvas
                if (allowUnpause) Time.timeScale = 1f;
            }
            else
            {
                // Enable and show the canvas if it is currently disabled
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;

                // Pause the game upon enabling the canvas
                Time.timeScale = 0f;
            }
        }
    }
}
