using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirection = 1.5f;
    public Transform area;
    public float edgeBuffer = 0.8f;

    private Vector3 direction;
    private float timer;
    private Rigidbody rb;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Animator anim;
    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    public float stuckCheckInterval = 0.5f;
    public float stuckDistanceThreshold = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        lastPosition = rb.position;
        PickNewDirection();
        if (area != null)
        {
            Vector3 areaCenter = area.position;
            Vector3 halfSize = area.localScale * 0.5f;
            minBounds = areaCenter - halfSize;
            maxBounds = areaCenter + halfSize;
        }
    }

    void FixedUpdate()
    {
        Vector3 nextPosition = rb.position + direction * speed * Time.fixedDeltaTime;
        if (area != null)
        {
            stuckTimer += Time.fixedDeltaTime;
            if (stuckTimer >= stuckCheckInterval)
            {
                float distanceMoved = Vector3.Distance(rb.position, lastPosition);
                if (distanceMoved < stuckDistanceThreshold)
                {
                    PickEscapeDirection();
                    timer = 0f;
                }
                lastPosition = rb.position;
                stuckTimer = 0f;
            }
            bool nearEdge = false;
            if (nextPosition.x < minBounds.x + edgeBuffer || nextPosition.x > maxBounds.x - edgeBuffer)
                nearEdge = true;
            if (nextPosition.z < minBounds.z + edgeBuffer || nextPosition.z > maxBounds.z - edgeBuffer)
                nearEdge = true;
            if (nearEdge)
            {
                PickNewDirection();
                timer = 0f;
            }
            if (anim != null)
            {
                float speed = rb.linearVelocity.magnitude;
                anim.SetFloat("Speed", speed); // Use your blend tree's parameter name here!
            }
            rb.linearVelocity = direction * speed;

            timer += Time.fixedDeltaTime;
            if (timer >= changeDirection)
            {
                PickNewDirection();
                timer = 0f;
            }

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                rb.MoveRotation(targetRotation);
            }
        }
    }

    void PickEscapeDirection()
    {
        if (area != null)
        {
            Vector3 areaCenter = area.position;
            Vector3 areaExtent = area.localScale * 0.5f;
            Vector3 escapeDir = (areaCenter - rb.position).normalized; // towards area center
            escapeDir.y = 0;
            if (escapeDir.sqrMagnitude < 0.1f)
            {
                // If very close to center, just pick a random direction
                PickNewDirection();
            }
            else
            {
                direction = escapeDir;
            }
        }
        else
        {
            PickNewDirection();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            PickNewDirection();
            timer = 0f;
        }
        // Fireball kill logic
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(gameObject); // Skeleton dies
            Destroy(collision.gameObject); // Optionally destroy the fireball too
        }
    }

    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);
        direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
}