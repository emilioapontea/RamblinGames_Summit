using UnityEngine;

public class MovingTile : MonoBehaviour
{
    public float moveDistance = 3f; // How far to move right
    public float speed = 2f;        // How fast to move

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveDistance;
    }

    void Update()
    {
        Vector3 destination = movingRight ? targetPos : startPos;

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            movingRight = !movingRight; // Flip direction
        }
    }
}
