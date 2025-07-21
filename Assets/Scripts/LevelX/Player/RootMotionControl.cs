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
    // private bool jumpPressed = false;

    [Header("Speed Control")]
    public float animationSpeed = 1.0f;
    public float rootMovementSpeed = 2.0f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public int maxWalljumps = 3;
    public float walljumpKickbackForce = 2f;
    public float walljumpVerticalMultiplier = 2f;
    public float walljumpDelay = 1f;

    private int walljumpCharges;
    private float walljumpTimer;
    private int walljumpWallContactCount = 0;
    private ContactPoint wallJumpContactPoint;
    private bool airborne;


    public bool IsGrounded => groundContactCount > 0;
    public bool CanWallJump => walljumpWallContactCount > 0 && walljumpCharges > 0 && walljumpTimer <= 0f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInputController>();

        walljumpCharges = maxWalljumps;
        walljumpTimer = walljumpDelay;

        anim.applyRootMotion = true;
    }

    void Update()
    {
        if (cinput.enabled)
        {
            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;

            // if (cinput.Jump && IsGrounded)
            // {
            //     jumpPressed = true;
            // }
        }

        // Debug.Log($"Charges: {walljumpCharges}");

        // if (airborne && rbody.linearVelocity.y == 0)
        if (IsGrounded)
        {
            airborne = false;
            walljumpCharges = maxWalljumps;
            // TODO: fix landing and jumping animations
            // anim.SetTrigger("land");
        }

        // Optional: runtime speed tweak
        // if (Input.GetKeyDown(KeyCode.Equals)) // '+'
        // {
        //     animationSpeed += 0.1f;
        //     rootMovementSpeed += 0.1f;
        // }
        // else if (Input.GetKeyDown(KeyCode.Minus)) // '-'
        // {
        //     animationSpeed = Mathf.Max(0.1f, animationSpeed - 0.1f);
        //     rootMovementSpeed = Mathf.Max(0.1f, rootMovementSpeed - 0.1f);
        // }

        anim.speed = animationSpeed;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void FixedUpdate()
    {
        anim.SetFloat("velx", _inputTurn);
        anim.SetFloat("vely", _inputForward);

        // I'm iffy about this...
        // if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
        // {
        //     transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z);
        // }

        // if (jumpPressed && isGrounded)
        // {
        //     // rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //     anim.SetTrigger("jump");
        //     jumpPressed = false;
        // }

        Vector3 jump = CheckJump();
        rbody.AddForce(jump, ForceMode.Impulse);

        walljumpTimer -= Time.deltaTime;
        // transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            groundContactCount++;

        }
        if (collision.transform.CompareTag("WalljumpWall"))
        {
            walljumpWallContactCount++;
            wallJumpContactPoint = collision.GetContact(0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            groundContactCount--;

        }
        if (collision.transform.CompareTag("WalljumpWall"))
        {
            walljumpWallContactCount--;
        }
    }

    Vector3 CheckJump()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            return Vector3.zero;
        }

        if (IsGrounded)
        {
            if (!airborne)
            {
                airborne = true;
            }

            walljumpTimer = walljumpDelay;
            // TODO: fix landing and jumping animations
            // anim.SetTrigger("airborne");
            return new Vector3(0, jumpForce, 0);
        }
        else if (CanWallJump)
        {
            walljumpCharges--;
            walljumpTimer = walljumpDelay;

            rbody.linearVelocity = new Vector3(rbody.linearVelocity.x, 0, rbody.linearVelocity.z);
            transform.rotation = Quaternion.LookRotation(new Vector3(wallJumpContactPoint.normal.x, 0, wallJumpContactPoint.normal.z), Vector3.up);

            Vector3 wallJumpVector = 1 * walljumpKickbackForce * wallJumpContactPoint.normal;
            Vector3 jumpVector = new Vector3(wallJumpVector.x, jumpForce * walljumpVerticalMultiplier, wallJumpVector.z);
            // TODO: fix landing and jumping animations
            // anim.SetTrigger("airborne");
            return jumpVector;
        }
        return Vector3.zero;
    }
}
