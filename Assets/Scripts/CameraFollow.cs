using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Offset in local space (always behind the car)
        Vector3 desiredPosition = target.position + target.right * offset.x + target.up * offset.y + target.forward * offset.z;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Look at the car
        transform.LookAt(target);
    }
}