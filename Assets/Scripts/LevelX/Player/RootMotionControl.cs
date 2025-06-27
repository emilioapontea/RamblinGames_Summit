using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class RootMotionControl : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    private int groundContactCount = 0;
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    private float _inputForward = 0f;
    private float _inputTurn = 0f;
    private bool jumpPressed = false;

    [Header("Speed Control")]
    public float animationSpeed = 1.0f;
    public float rootMovementSpeed = 1.0f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;

    public bool IsGrounded => groundContactCount > 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInputController>();

        anim.applyRootMotion = true;
    }

    void Update()
    {
        if (cinput.enabled)
        {
            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;

            if (cinput.Jump && IsGrounded)
            {
                jumpPressed = true;
            }
        }

        // Optional: runtime speed tweak
        if (Input.GetKeyDown(KeyCode.Equals)) // '+'
        {
            animationSpeed += 0.1f;
            rootMovementSpeed += 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Minus)) // '-'
        {
            animationSpeed = Mathf.Max(0.1f, animationSpeed - 0.1f);
            rootMovementSpeed = Mathf.Max(0.1f, rootMovementSpeed - 0.1f);
        }

        anim.speed = animationSpeed;
    }

    void FixedUpdate()
    {
        bool isGrounded = IsGrounded;

        anim.SetFloat("velx", _inputTurn);
        anim.SetFloat("vely", _inputForward);
        anim.SetBool("isFalling", !isGrounded);

        if (jumpPressed && isGrounded)
        {
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("jump");  // 🟢 Only works if Animator has this trigger
            jumpPressed = false;
        }
    }

    void OnAnimatorMove()
    {
        // Only apply horizontal root motion, preserve vertical motion
        Vector3 rootDelta = anim.deltaPosition;
        rootDelta.y = 0f;

        Vector3 move = rbody.position + rootDelta * rootMovementSpeed;
        move.y = rbody.position.y; // keep physics-controlled Y

        rbody.MovePosition(move);
        rbody.MoveRotation(anim.rootRotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            groundContactCount++;
            Debug.Log($"Landed, ground contact: {groundContactCount}");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            groundContactCount--;
            Debug.Log($"Left Ground, ground contact: {groundContactCount}");
        }
    }
}
