using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Unity.Mathematics;

public class PlayerControllerDungeon : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 700f;

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {   
            Debug.Log("f pressed, initiating attack.");
            Attack();
        }
    }

    void Attack()
    {
        // Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + transform.forward * 1.0f;
        // Quaternion spawnRot = firePoint != null ? firePoint.rotation : transform.rotation;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, transform.rotation);
        Rigidbody fireballRb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 inheritedVelocity = rb != null ? rb.linearVelocity : Vector3.zero;

            Vector3 fireballVelocity = inheritedVelocity + transform.forward * fireballSpeed;
            fireballRb.linearVelocity = fireballVelocity;
        }
    }
}
