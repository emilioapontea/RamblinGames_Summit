using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirection = 1f;
    public Transform area;
    public float edgeBuffer = 0.3f; // Distance from edge to start turning

    private Vector3 direction;
    private float timer;
    private Rigidbody rb;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        // Predict next position based on current direction and speed
        Vector3 nextPosition = rb.position + direction * speed * Time.fixedDeltaTime;

        if (area != null)
        {
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            PickNewDirection();
            timer = 0f;
        }
    }

    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);
        direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
}