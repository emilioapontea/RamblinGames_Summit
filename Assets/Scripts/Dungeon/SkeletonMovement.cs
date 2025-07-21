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
        rb.constraints |= RigidbodyConstraints.FreezePositionY;
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
        // Clamp the skeleton's position to keep it strictly inside the bounding area (zero tolerance)
        if (area != null)
        {
            Vector3 clampedPosition = rb.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
            clampedPosition.y = rb.position.y; // Don't clamp Y, allow natural gravity/jumping
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minBounds.z, maxBounds.z);
            rb.position = clampedPosition;
        }

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
        }

        // Animation
        if (anim != null)
        {
            float currentSpeed = rb.linearVelocity.magnitude;
            anim.SetFloat("Speed", currentSpeed);
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

    void PickEscapeDirection()
    {
        if (area != null)
        {
            Vector3 areaCenter = area.position;
            Vector3 escapeDir = (areaCenter - rb.position).normalized;
            escapeDir.y = 0;
            if (escapeDir.sqrMagnitude < 0.01f)
            {
                PickRandomDirection();
            }
            else
            {
                direction = escapeDir;
            }
        }
        else
        {
            PickRandomDirection();
        }
    }

    void PickRandomDirection()
    {
        Vector3 newDir;
        int tries = 0;
        do
        {
            float angle = Random.Range(0f, 360f);
            newDir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            tries++;
        } while (Vector3.Dot(newDir, direction) > 0.85f && tries < 10);
        direction = newDir;
    }

    void PickNewDirection()
    {
        PickRandomDirection();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            PickNewDirection();
            timer = 0f;
        }
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}