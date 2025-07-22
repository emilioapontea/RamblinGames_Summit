using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInputController : MonoBehaviour
{

    [Header("Filtering & Input")]
    public bool InputMapToCircular = true;
    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    [Header("Keyboard Analog Control")]
    public float forwardSpeedLimit = 1f;

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public float Forward { get; private set; }
    public float Turn { get; private set; }
    public bool Action { get; private set; }
    public bool Jump { get; private set; }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Optional: map square inputs to circle for smoother blends
        if (InputMapToCircular)
        {
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);
        }

        // BEGIN ANALOG ON KEYBOARD DEMO CODE
        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f;

        if (Input.GetKeyUp(KeyCode.Alpha1)) forwardSpeedLimit = 0.1f;
        else if (Input.GetKeyUp(KeyCode.Alpha2)) forwardSpeedLimit = 0.2f;
        else if (Input.GetKeyUp(KeyCode.Alpha3)) forwardSpeedLimit = 0.3f;
        else if (Input.GetKeyUp(KeyCode.Alpha4)) forwardSpeedLimit = 0.4f;
        else if (Input.GetKeyUp(KeyCode.Alpha5)) forwardSpeedLimit = 0.5f;
        else if (Input.GetKeyUp(KeyCode.Alpha6)) forwardSpeedLimit = 0.6f;
        else if (Input.GetKeyUp(KeyCode.Alpha7)) forwardSpeedLimit = 0.7f;
        else if (Input.GetKeyUp(KeyCode.Alpha8)) forwardSpeedLimit = 0.8f;
        else if (Input.GetKeyUp(KeyCode.Alpha9)) forwardSpeedLimit = 0.9f;
        else if (Input.GetKeyUp(KeyCode.Alpha0)) forwardSpeedLimit = 1.0f;
        // END ANALOG ON KEYBOARD DEMO CODE

        // Apply smoothing and clamping
        filteredForwardInput = Mathf.Clamp(
            Mathf.Lerp(filteredForwardInput, v, Time.deltaTime * forwardInputFilter),
            -forwardSpeedLimit, forwardSpeedLimit
        );

        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h, Time.deltaTime * turnInputFilter);

        Forward = filteredForwardInput;
        Turn = filteredTurnInput;

        // Fire1 = Action (mouse click, space, etc.)
        Action = Input.GetButtonDown("Fire1");
        Jump = Input.GetButtonDown("Jump");

        // Debug skip key
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Skipping to next level...");
            // Implement level skip logic here
            // For example, load the next scene or reset the current one
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
