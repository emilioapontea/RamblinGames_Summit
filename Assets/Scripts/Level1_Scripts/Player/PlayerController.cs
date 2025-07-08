using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float velocityX;
    private float velocityY;
    private float velocityZ;
    [Header("Player Parameters")]
    /// <summary>
    /// Magnitude of force applied to player while moving laterally.
    /// </summary>
    public float movementSpeed = 2.0f;
    /// <summary>
    /// Magnitude of vertical force applied to player upon jumping.
    /// </summary>
    public float jumpPower = 10.0f;
    /// <summary>
    /// The rate (in degrees) at which the player will turn on every fixed update.
    /// </summary>
    public float turningSpeed = 2.0f;
    [Header("Player SFX")]
    /// <summary>
    /// Set this field to false to prevent the player from playing sound effects.
    /// </summary>
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
    private bool airborne;
    [Header("References")]
    public LoseHandler loseScript;
    [Header("Debug")]
    /// <summary>
    /// Show the physics raycast used to test if player is grounded?
    /// Gizmos must also be set to visible for raycast to be seen.
    /// </summary>
    [SerializeField] private bool showGroundedRaycast = false;
    /// <summary>
    /// Draw a raycast in the direction the player is facing?
    /// Gizmos must also be set to visible for raycast to be seen.
    /// </summary>
    [SerializeField] private bool showDirectionRaycast = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (loseScript == null)
        {
            Debug.LogError("Player object could not find a lose game script.");
        }
        // Allow death SFX to play even when game is paused
        deathPlayer.ignoreListenerPause = true;
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Turn();

        Vector3 movement = (transform.forward * inputZ) + (transform.right * inputX);
        rb.AddForce(movement * movementSpeed);

        Vector3 jump = CheckJump();
        rb.AddForce(jump, ForceMode.Impulse);
    }

    void Update()
    {
        if (showGroundedRaycast)
        {
            if (IsGrounded())
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
            if (playSoundEffects) landPlayer.Play();
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

    bool IsGrounded()
    {
        // Allow the jump if the player is grounded (i.e., is in contact with the floor)
        // To allow for the player to jump higher by holding the jump button,
        // the raycast should extend slightly below the player.
        // This allows the player to hold the jump button to extend the jump.
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 CheckJump()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
        //Allow the jump if the player is grounded (i.e., is in contact with the floor)
        if (IsGrounded())
        {
            if (!airborne)
            {
                // Play the jumping sound effect once upon jumping
                // Airborne should only be reset to false upon landing
                airborne = true;
                if (playSoundEffects) jumpPlayer.Play();
            }
            return new Vector3(0.0f, jumpPower, 0.0f);
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
            transform.Rotate(0.0f, -1 * turningSpeed, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.P))
        {
            transform.Rotate(0.0f, turningSpeed, 0.0f, Space.Self);
        }
    }

    void Death()
    {
        if (playSoundEffects) deathPlayer.Play();
        loseScript.LoseGame();
    }
}
