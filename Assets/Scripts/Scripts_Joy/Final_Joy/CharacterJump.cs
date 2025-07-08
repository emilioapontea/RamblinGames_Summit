using UnityEngine;

public class VariableJump : MonoBehaviour
{
    public float initialJumpForce = 7f;      // Force applied at jump start
    public float holdJumpForce = 3f;         // Continuous force while holding jump
    public float maxHoldTime = 0.5f;         // Max time to apply continuous force
    public float maxJumpHeight = 5f;         // Optional: max height you want to restrict

    private Rigidbody rb;
    private bool isJumping = false;
    private float holdTime = 0f;
    private float startY;                    // To track max jump height

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (isJumping)
        {
            if (Input.GetKey(KeyCode.Space) && holdTime < maxHoldTime && transform.position.y < startY + maxJumpHeight)
            {
                // Apply small upward force while space is held and max height not reached
                rb.AddForce(Vector3.up * holdJumpForce * Time.deltaTime, ForceMode.VelocityChange);
                holdTime += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);  // Reset vertical velocity for consistent jump
            rb.AddForce(Vector3.up * initialJumpForce, ForceMode.VelocityChange);
            isJumping = true;
            holdTime = 0f;
            startY = transform.position.y;
        }
    }
}
