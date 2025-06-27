using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class ElephantControlScript : MonoBehaviour
{
    //private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    public float forwardMaxSpeed = 5f;
    public float turnMaxSpeed = 720f;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInputController>();
    }

    void Start()
    {
        //anim.applyRootMotion = false;
    }

    void FixedUpdate()
    {
        float inputForward = cinput.Forward;
        float inputTurn = cinput.Turn;

        // Keep the existing vertical velocity (for gravity)
        Vector3 velocity = rbody.linearVelocity;
        Vector3 moveDirection = transform.forward * inputForward * forwardMaxSpeed;
        rbody.linearVelocity = new Vector3(moveDirection.x, velocity.y, moveDirection.z);

        // Rotate
        rbody.MoveRotation(rbody.rotation * Quaternion.Euler(0f, inputTurn * turnMaxSpeed * Time.fixedDeltaTime, 0f));
    }
}
