using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    /// <summary>
    /// The player's visible character model. This should not be the player's collision.
    /// </summary>
    [Header("Player Model Settings")]
    public GameObject playerModel;
    /// <summary>
    /// Speed at which the visible player model should turn towards the direction of movement.
    /// </summary>
    public float playerModelTurnSpeed = 10.0f;
    /// <summary>
    /// Magnitude of force applied to player while moving laterally.
    /// </summary>
    [Header("Player Parameters")]
    public float movementSpeed = 2.0f;
    /// <summary>
    /// Magnitude of vertical force applied to player upon jumping.
    /// </summary>
    public float jumpPower = 10.0f;
    /// <summary>
    /// The rate (in degrees) at which the camera will turn on every fixed update 
    /// when a 'Turn Camera' key is pressed.
    /// </summary>
    public float cameraTurningSpeed = 2.0f;
    /// <summary>
    /// Magnitude of force applied in direction opposite of movement when the
    /// player character is grounded and no player input is being made.
    /// </summary>
    public float decelerationForce = 2.0f;
    /// <summary>
    /// How many walljumps the player can perform on a jumpable wall before
    /// the player must land on the ground to recharge walljumps.
    /// </summary>
    public int maxWalljumps = 3;
    /// <summary>
    /// How much force will be applied in the direction opposite the wall the player jumps off
    /// when the player performs a walljump.
    /// </summary>
    public float walljumpKickbackForce = 2.0f;
    /// <summary>
    /// The multiplier to apply to the vertical component of the walljump force vector.
    /// </summary>
    public float walljumpVerticalMultiplier = 2.0f;
    /// <summary>
    /// How much time (in seconds) that must pass after a jump or walljump is performed
    /// for the player to be able to perform another walljump
    /// (assuming the player still has walljump charges remaining).
    /// </summary>
    public float walljumpDelay = 1.0f;
    private float walljumpTimer;
    private int walljumpCharges;
    private ContactPoint walljumpContactPoint;
    /// <summary>
    /// Set this field to false to prevent the player from playing sound effects.
    /// </summary>
    [Header("Player SFX")]
    [SerializeField] private bool playSoundEffects = true;
    /// <summary>
    /// The Audio Source that plays the jumping sound effect.
    /// </summary>
    public AudioSource jumpPlayer;
    /// <summary>
    /// The Audio Source that plays the landing sound effect.
    /// </summary>
    public AudioSource landPlayer;
    /// <summary>
    /// The audio source that plays the player death sound effect.
    /// </summary>
    public AudioSource deathPlayer;
    /// <summary>
    /// Used to check when the player transitions from grounded to jumping,
    /// and from airborne to grounded. This field is only used for the purposes
    /// of playing sound effects.
    /// </summary>
    private Animator anim;
    /// <summary>
    /// Max speed of playern run animation. Animation speed is set to 1 when player is motionless,
    /// and is linearlly interpolated to this value as the player gains speed so that the running
    /// animation speeds up as the player gains speed.
    /// </summary>
    [Header("Player Animation")]
    public float maxAnimationSpeed = 3f;
    private int groundContactCount = 0;
    private bool Grounded => groundContactCount > 0;
    private bool airborne;
    private int walljumpWallContactCount = 0;
    private bool CanWalljump => (walljumpWallContactCount > 0 && walljumpCharges > 0 && walljumpTimer <= 0f);
    [Header("References")]
    public LoseHandler loseScript;
    /// <summary>
    /// Show the physics raycast used to test if player is grounded?
    /// Gizmos must also be set to visible for raycast to be seen.
    /// </summary>
    [Header("Debug")]
    [SerializeField] private bool showGroundedRaycast = false;
    /// <summary>
    /// Draw a raycast in the direction the player is facing?
    /// Gizmos must also be set to visible for raycast to be seen.
    /// </summary>
    [SerializeField] private bool showDirectionRaycast = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        walljumpCharges = maxWalljumps;
        walljumpTimer = walljumpDelay;
        anim = GetComponentInChildren<Animator>();
        if (loseScript == null) Debug.LogError("PlayerController: Player object could not find a lose game script.");
        if (playerModel == null) Debug.LogError("PlayerController: Player object could not find a reference to a visible model.");
        if (anim == null) Debug.LogError("PlayerController: Player object could not find animator component in children.");
        // Allow death SFX to play even when game is paused
        deathPlayer.ignoreListenerPause = true;
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Turn();

        Vector3 movement = (transform.forward * inputZ) + (transform.right * inputX);
        if (movement.magnitude > 0f)
        {
            rb.AddForce(movement * movementSpeed);
            // Turn player model towards direction of movement
            RotatePlayerModel(movement);
        }
        else if (Grounded)
        {
            // Only allow deceleration while player is grounded
            // No input is being made, decelerate the player character
            rb.AddForce(-1 * decelerationForce * rb.linearVelocity);
        }

        float animationVelocity = Mathf.Lerp(0f, 1f, rb.linearVelocity.magnitude / 10);
        anim.SetFloat("velocity", Mathf.Lerp(0f, 1f, animationVelocity));
        anim.SetFloat("animSpeed", Mathf.Lerp(1f, maxAnimationSpeed, animationVelocity));

        Vector3 jump = CheckJump();
        rb.AddForce(jump, ForceMode.Impulse);

        walljumpTimer -= Time.deltaTime; // Decrement walljump timer
    }

    void Update()
    {
        if (showGroundedRaycast)
        {
            if (Grounded)
            {
                // Draw blue raycast if player can jump
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10, Color.blue);
            }
            else
            {
                // Draw red raycast if player cannot jump
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10, Color.red);
            }
        }
        if (showDirectionRaycast)
        {
            // Draw raycast in the direction player is facing
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }

        if (airborne && rb.linearVelocity.y == 0)
        {
            // Play the landing sound effect on the first frame the player is no longer airborne
            airborne = false;
            anim.SetTrigger("land");
            if (playSoundEffects) landPlayer.Play();
            walljumpCharges = maxWalljumps; // Reset walljump charges
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            deathPlayer.Play();
            Death();
        }
    }

    // bool IsGrounded()
    // {
    //     // Allow the jump if the player is grounded (i.e., is in contact with the floor)
    //     // To allow for the player to jump higher by holding the jump button,
    //     // the raycast should extend slightly below the player.
    //     // This allows the player to hold the jump button to extend the jump.
    //     if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    Vector3 CheckJump()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
        //Allow the jump if the player is grounded (i.e., is in contact with the floor)
        if (Grounded)
        {
            if (!airborne)
            {
                // Play the jumping sound effect once upon jumping
                // Airborne should only be reset to false upon landing
                airborne = true;
                anim.SetTrigger("airborne");
                if (playSoundEffects) jumpPlayer.Play();
            }
            walljumpTimer = walljumpDelay;
            return new Vector3(0.0f, jumpPower, 0.0f);
        }
        else if (CanWalljump)
        {
            // Allow the player to wall jump if the player is in contact with
            // a wall that can be jumped off of, and the player has one or more
            // remaining walljump charges.
            if (playSoundEffects) jumpPlayer.Play();
            walljumpCharges--;
            walljumpTimer = walljumpDelay;
            // Reset the player's vertical velocity so that the walljump gains a consistent amount of height
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            // Send the player away from the wall
            Vector3 wallJumpVector = 1 * walljumpKickbackForce * walljumpContactPoint.normal;
            Vector3 jumpVector = new Vector3(wallJumpVector.x, jumpPower * walljumpVerticalMultiplier, wallJumpVector.z);
            return jumpVector;
        }
        else
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    void Turn()
    {
        if (Input.GetKey(KeyCode.O))
        {
            transform.Rotate(0.0f, -1 * cameraTurningSpeed, 0.0f, Space.Self);
            // Rotate player model in opposite direction to prevent player model from 
            // turning when camera spins
            playerModel.transform.Rotate(0.0f, cameraTurningSpeed, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.P))
        {
            transform.Rotate(0.0f, cameraTurningSpeed, 0.0f, Space.Self);
            // Rotate player model in opposite direction to prevent player model from 
            // turning when camera spins
            playerModel.transform.Rotate(0.0f, -1 * cameraTurningSpeed, 0.0f, Space.Self);
        }
    }

    void RotatePlayerModel(Vector3 targetVector)
    {
        if (playerModel == null) return;
        // Don't rotate if moving slowly
        if (targetVector.magnitude <= 0.1f) return;
        Quaternion targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);
        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,
            targetRotation, playerModelTurnSpeed * Time.deltaTime);
    }

    void Death()
    {
        if (playSoundEffects) deathPlayer.Play();
        loseScript.LoseGame();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            groundContactCount++;
        }
        if (collision.transform.CompareTag("WalljumpWall"))
        {
            walljumpWallContactCount++;
            // Set walljump Contact Point reference to latest point of contact
            walljumpContactPoint = collision.GetContact(0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            groundContactCount--;
        }

        if (collision.transform.CompareTag("WalljumpWall"))
        {
            walljumpWallContactCount--;
        }
    }
}
