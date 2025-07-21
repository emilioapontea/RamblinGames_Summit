using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 700f;

    void update()
    {
        if (Input.GetKey(KeyCode.F))
        {   
            Debug.Log("Space key pressed, initiating attack.");
            Attack();
        }
    }

    void Attack()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            Debug.Log("Fireball instantiated and Rigidbody2D found.");
            rb.AddForce(firePoint.up * fireballSpeed);
        }
    }
}