using UnityEditor.UI;
using UnityEngine;

public class IceBlockController : MonoBehaviour
{
    public Vector3 startPosition;          // Initial position of the block
    public float pushThreshold = 2f;       // How much contact time counts as "push"
    public float slideSpeed = 5f;            // Speed at which block slides
    public LayerMask obstacleMask;           // To detect obstacles
    private Animator anim;              // Animator for the block

    // Through tuning (!) found 0.5 is perfect for the ice blocks I'm using.
    // For forward compatibility, this approach should change...
    private float rayDistanceMult = 0.5f;         // Distance to check for obstacles
    private float rayDistance;                // Actual distance for raycasting

    private bool isSliding;
    public bool IsSliding
    {
        get { return isSliding; }
        // set { isSliding = value; }
    }
    private Vector3 slideDirection;
    private float contactTime = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();

        this.Reset();
    }

    void FixedUpdate()
    {
        if (isSliding)
        {
            // Check for obstacle in front
            if (Physics.Raycast(this.transform.position, this.slideDirection, this.rayDistance, this.obstacleMask))
            {
                // Snap to the nearest grid position
                Vector3 snappedPosition = new Vector3(
                    Mathf.Round(this.transform.position.x),
                    this.transform.position.y,
                    Mathf.Round(this.transform.position.z)
                );
                this.transform.position = snappedPosition;
                this.isSliding = false;
                return;
            }

            // Slide in fixed direction
            this.transform.position += this.slideDirection * this.slideSpeed * Time.deltaTime;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
        {
            Destroy(this.gameObject); // Destroy the block when the exit animation is done
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (this.isSliding) return;
        this.contactTime = 0f; // Reset contact time on new collision
    }

    void OnCollisionStay(Collision collision)
    {
        if (this.isSliding) return;
        // Detect push direction based on player contact
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.contactTime > this.pushThreshold)
            {
                Vector3 direction = (this.transform.position - collision.transform.position).normalized;

                // Determine axis-aligned slide direction
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
                {
                    this.slideDirection = new Vector3(Mathf.Sign(direction.x), 0, 0);
                }
                else
                {
                    this.slideDirection = new Vector3(0, 0, Mathf.Sign(direction.z));
                }

                this.isSliding = true;
            }
            else
            {
                // Increase contact time while in contact
                this.contactTime += Time.deltaTime;
                // Debug.Log($"Contact time: {contactTime}");
            }

            // Debug.Log($"Collision with player detected. Contact speed: {collision.relativeVelocity.magnitude}");
            if (collision.relativeVelocity.magnitude <= 0f)
            {
                // Reset contact time if player is not moving
                this.contactTime = 0f;
                // Debug.Log("Resetting contact time due to low velocity");
            }
        }
    }

    public void Reset()
    {
        this.isSliding = false;

        // Initialize the block's position (relative to parent)
        float scale = transform.localScale.x;
        this.transform.position = this.transform.parent.position + (this.startPosition + new Vector3(0f, 0.5f, 0f)) * scale;
        // Calculate ray distance based on block size
        this.rayDistance = scale * this.rayDistanceMult;
        anim.SetBool("solved", false);
    }

    public void OnPuzzleSolved()
    {
        anim.SetBool("solved", true);
    }
}
