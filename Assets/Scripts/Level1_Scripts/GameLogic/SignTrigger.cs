using UnityEngine;

public class SignTrigger : MonoBehaviour
{
    public CanvasGroup signCanvas;
    void Awake()
    {
        if (signCanvas == null)
        {
            Debug.LogError("Sign parent object could not find canvas group in children.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Make sign canvas visible
            signCanvas.interactable = true;
            signCanvas.blocksRaycasts = true;
            signCanvas.alpha = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Make sign canvas invisible
            signCanvas.interactable = false;
            signCanvas.blocksRaycasts = false;
            signCanvas.alpha = 0f;
        }
    }
}
