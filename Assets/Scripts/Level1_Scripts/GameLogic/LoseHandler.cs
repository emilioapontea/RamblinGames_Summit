using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseHandler : MonoBehaviour
{
    private CanvasGroup loseCanvas;
    private bool loseState = false;
    void Start()
    {
        loseCanvas = GetComponent<CanvasGroup>();
        if (loseCanvas == null) Debug.LogError("LoseHandler does not have a canvas group.");
    }

    void Update()
    {
        if (loseState && Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        loseCanvas.alpha = 1f;
        loseCanvas.interactable = true;
        loseCanvas.blocksRaycasts = true;
        loseState = true;
    }
}
