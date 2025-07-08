using UnityEngine;

public class DrawbridgeTrigger : MonoBehaviour
{
    public DrawbridgeAnimator drawbridgeAnimator;

    void Start()
    {
        if (drawbridgeAnimator == null)
        {
            Debug.LogError("Drawbridge trigger has no attached reference to drawbridge animator!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Lower the drawbridge when a player object enters the trigger zone
        if (other.CompareTag("Player"))
        {
            drawbridgeAnimator.LowerBridge();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Raise the drawbridge when a player object leaves the trigger zone
        if (other.CompareTag("Player"))
        {
            drawbridgeAnimator.RaiseBridge();
        }
    }
}
