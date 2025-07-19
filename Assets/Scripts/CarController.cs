using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 555.0f;
    public float turnSpeed = 50f;
    public float bounceForce = 10f;
    public float bufferDistance = 0.5f;

    void Update()
    {
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        float move = -Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Move forward/backward
        transform.position += transform.right * move;

        // Turn left/right
        transform.Rotate(0, turn, 0);

        MaintainBufferZone();
    }

    void MaintainBufferZone()
    {
        Vector3[] directions = {
            transform.forward,
            -transform.forward,
            transform.right,
            -transform.right
        };

        foreach (var dir in directions)
        {
            Ray ray = new Ray(transform.position, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, bufferDistance))
            {
                // Only bounce if the hit is not the car itself
                if (hit.collider.gameObject != gameObject)
                {
                    Vector3 bounceDir = (transform.position - hit.point).normalized;
                    transform.position += bounceDir * bounceForce * Time.deltaTime;
                }
            }
        }
    }
}