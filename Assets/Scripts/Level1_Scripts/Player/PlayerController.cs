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
    public Camera CarCam;
    public GameObject EnemyCar;
    public TMPro.TMP_Text lapCounterText; 
    private int currentLap = 0;

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
    private bool isNearCar;
    [Header("References")]
    public LoseHandler loseScript;
    public GameObject playerCar; 
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
        lapCounterText.gameObject.SetActive(false); // Hide lap counter at start
        EnemyCar.SetActive(false); // Disable enemy car at start
        rb = GetComponent<Rigidbody>();
        if (loseScript == null)
        {
            Debug.LogError("Player object could not find a lose game script.");
        }

        if (playerCar != null)
        {
            playerCar.GetComponent<CarController>().enabled = false; // Disable car control at start
        }
        isNearCar = false;
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

        if(isNearCar && Input.GetKeyDown(KeyCode.E))
        {
            EnterCar();
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
        if(other.CompareTag("Finish"))
        {
            Debug.Log("Player has completed a lap!");
            CompleteLap();
        }
        if (other.CompareTag("PlayerCar"))
        {
            Debug.Log("Player is near car");
            isNearCar = true;
        }
        if (other.CompareTag("Respawn"))
        {
            deathPlayer.Play();
            Death();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            isNearCar = false;
        }
    }

    void CompleteLap() {
        currentLap++;
        if (lapCounterText != null)
        {
            lapCounterText.text = $"Lap: {currentLap}/1"; // Assuming 3 laps for this example
        }
        if (currentLap >= 2)
        {
            ExitCar();
        }
    }

    void ExitCar()
    {
        transform.SetParent(null);
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = true;
        }
        // Enable player movement
        rb.isKinematic = false;
        this.enabled = true; 
        playerCar.GetComponent<CarController>().enabled = false;
        Camera.main.enabled = true;
        CarCam.enabled = false;
        EnemyCar.SetActive(false);
        lapCounterText.gameObject.SetActive(false);
    }

    void EnterCar()
    {
        transform.rotation = playerCar.transform.rotation; 
        transform.position = playerCar.transform.position + Vector3.up * 1.5f; // Adjust height as needed
        transform.SetParent(playerCar.transform);
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
        // Disable player movement
        rb.isKinematic = true;
        this.enabled = false; 
        playerCar.GetComponent<CarController>().enabled = true;
        SwitchCameraToCar();
        Camera.main.enabled = false;
        CarCam.enabled = true;
        EnemyCar.SetActive(true);
        lapCounterText.gameObject.SetActive(true);
        lapCounterText.text = $"Lap: {currentLap}/1";
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

    void SwitchCameraToCar()
    {
        Camera.main.GetComponent<CameraFollow>().target = playerCar.transform;
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
