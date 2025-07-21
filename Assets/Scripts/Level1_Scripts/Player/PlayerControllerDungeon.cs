using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Unity.Mathematics;
using System.Collections;

public class PlayerControllerDungeon : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 700f;
    public float fireballTimeout = 0.5f;
    private float fireballTimer = 0f;
    private bool enableAttack = false;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Return) && fireballTimer <= 0f && enableAttack)
        {
            // Debug.Log("f pressed, initiating attack.");
            Attack();
            fireballTimer = fireballTimeout;
        }
        else if (fireballTimer > 0f)
        {
            fireballTimer -= Time.fixedDeltaTime;
        }
    }

    void Attack()
    {
        // Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + transform.forward * 1.0f;
        // Quaternion spawnRot = firePoint != null ? firePoint.rotation : transform.rotation;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, transform.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 inheritedVelocity = rb != null ? rb.linearVelocity : Vector3.zero;

            Vector3 fireballVelocity = inheritedVelocity + transform.forward * fireballSpeed;
            rb.linearVelocity = fireballVelocity;
        }
        StartCoroutine(DelayDestroy(fireball, 1f));
    }

    private IEnumerator DelayDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireballDoorTrigger")) enableAttack = true;
    }
}
