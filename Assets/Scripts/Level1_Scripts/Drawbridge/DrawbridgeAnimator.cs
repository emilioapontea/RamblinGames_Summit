using System;
using UnityEngine;

public class DrawbridgeAnimator : MonoBehaviour
{
    /// <summary>
    /// Reference to drawbridge's animator.
    /// </summary>
    private Animator bridgeAnimator;
    public DrawbridgeAudioPlayer audioPlayer;
    /// <summary>
    /// Should debug log messages be printed?
    /// If disabled, debug error messages will still be printed.
    /// </summary>
    [SerializeField] private bool debugMessages = false;

    void Start()
    {
        bridgeAnimator = GetComponent<Animator>();
        if (bridgeAnimator == null) Debug.LogError("Drawbride has no Animator component!");
    }

    public void RaiseBridge()
    {
        if (debugMessages) Debug.Log("Raising Bridge");
        audioPlayer.PlayBridgeSound();
        bridgeAnimator.ResetTrigger("Lower");
        bridgeAnimator.SetTrigger("Raise");
    }

    public void LowerBridge()
    {
        if (debugMessages) Debug.Log("Lowering Bridge");
        audioPlayer.PlayBridgeSound();
        bridgeAnimator.ResetTrigger("Raise");
        bridgeAnimator.SetTrigger("Lower");
    }
}
