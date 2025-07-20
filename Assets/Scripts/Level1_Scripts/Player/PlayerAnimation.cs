using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private Animator playerAnimator;
    private enum PlayerAnimationState { grounded, airborne }
    private PlayerAnimationState state = PlayerAnimationState.grounded;
    /// <summary>
    /// Max speed of playern run animation. Animation speed is set to 1 when player is motionless,
    /// and is linearlly interpolated to this value as the player gains speed so that the running
    /// animation speeds up as the player gains speed.
    /// </summary>
    public float maxAnimationSpeed = 3f;
    /// <summary>
    /// Set this field to false to prevent the player from playing sound effects.
    /// </summary>
    [Header("Player SFX")]
    [SerializeField] private bool playSoundEffects = true;
    /// <summary>
    /// The audio source that plays the player landing sound effect.
    /// </summary>
    public AudioSource landPlayer;
    void Awake()
    {
        if (playSoundEffects)
        {          
            if (landPlayer == null) Debug.LogError("PlayerAnimation: Could not find an audio source for landing sound effect.");
        }
        playerControllerScript = GetComponentInParent<PlayerController>();
        if (playerControllerScript == null) Debug.LogError("PlayerAnimation: Could not find PlayerController script in parent object.");
        playerAnimator = GetComponent<Animator>();
        if (playerAnimator == null) Debug.LogError("PlayerAnimation: Could not find animator component in this game object");
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case PlayerAnimationState.grounded:
                if (!playerControllerScript.GetGrounded())
                {
                    Jump();
                    state = PlayerAnimationState.airborne;
                }
                break;
            case PlayerAnimationState.airborne:
                if (playerControllerScript.GetGrounded())
                {
                    Land();
                    state = PlayerAnimationState.grounded;
                }
                break;
        }
    }

    public void Run(float runSpeed)
    {
        if (state != PlayerAnimationState.grounded) return;
        playerAnimator.SetFloat("velocity", Mathf.Lerp(0f, 1f, runSpeed));
        playerAnimator.SetFloat("animSpeed", Mathf.Lerp(1f, maxAnimationSpeed, runSpeed));
    }

    public void Jump()
    {
        if (state != PlayerAnimationState.grounded) return;
        playerAnimator.SetTrigger("airborne");
    }

    public void Land()
    {
        if (state != PlayerAnimationState.airborne) return;
        if (playSoundEffects) landPlayer.Play();
        playerAnimator.SetTrigger("land");
    }
}
