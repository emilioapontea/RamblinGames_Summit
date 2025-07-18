using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private Animator playerAnimator;
    public float animationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerControllerScript = GetComponentInParent<PlayerController>();
        if (playerControllerScript == null) Debug.LogError("PlayerAnimation: Could not find PlayerController script in parent.");
        playerAnimator = GetComponent<Animator>();
        if (playerAnimator == null) Debug.LogError("PlayerAnimation: Could not find animator component in this game object");
    }
}
