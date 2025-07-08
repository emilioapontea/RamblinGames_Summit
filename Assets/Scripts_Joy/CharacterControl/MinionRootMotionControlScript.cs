// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;


// // [RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
// // [RequireComponent(typeof(CharacterInputController))]
// // public class MinionRootMotionControlScript : MonoBehaviour
// // {
// //     private Animator anim;	
// //     private Rigidbody rbody;
// //     private CharacterInputController cinput;

// //     private int groundContactCount = 0;

// //     public bool IsGrounded
// //     {
// //         get
// //         {
// //             return groundContactCount > 0;
// //         }
// //     }

// //     float _inputForward = 0f;
// //     float _inputTurn = 0f;

// //     //Useful if you implement jump in the future...
// //     public float jumpableGroundNormalMaxAngle = 45f;
// //     public bool closeToJumpableGround;

// //     void Awake()
// //     {

// //         anim = GetComponent<Animator>();

// //         if (anim == null)
// //             Debug.Log("Animator could not be found");

// //         rbody = GetComponent<Rigidbody>();

// //         if (rbody == null)
// //             Debug.Log("Rigid body could not be found");

// //         cinput = GetComponent<CharacterInputController>();

// //         if (cinput == null)
// //             Debug.Log("CharacterInputController could not be found");

// //         anim.applyRootMotion = true;
// //     }


// //     void Start()
// //     {

// //     }


// //     private void Update()
// //     {
// //         if (cinput.enabled)
// //         {
// //             _inputForward = cinput.Forward;
// //             _inputTurn = cinput.Turn;
// //         }
// //     }

// //     void FixedUpdate()
// //     {

// //         //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
// //         //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
// //         //Therefore, an additional raycast approach is used to check for close ground
// //         bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.85f, 0f, out closeToJumpableGround);


// //         anim.SetFloat("velx", _inputTurn); 
// //         anim.SetFloat("vely", _inputForward);
// //         anim.SetBool("isFalling", !isGrounded);
// //     }



// //     //This is a physics callback
// //     void OnCollisionEnter(Collision collision)
// //     {
// //         if (collision.transform.gameObject.tag == "ground")
// //         {
// //             ++groundContactCount;
                         
// //             EventManager.TriggerEvent<MinionLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);
// //         }
						
// //     }

// //     private void OnCollisionExit(Collision collision)
// //     {
// //         if (collision.transform.gameObject.tag == "ground")
// //         {
// //             --groundContactCount;
// //         }
// //     }



// //     void OnAnimatorMove()
// //     {

// //         Vector3 newRootPosition;
// //         Quaternion newRootRotation;

// //         bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.85f, 0f, out closeToJumpableGround);

// //         if (isGrounded)
// //         {
// //             //use root motion as is if on the ground        
// //             newRootPosition = anim.rootPosition;
// //         }
// //         else
// //         {
// //             //Simple trick to keep model from climbing other rigidbodies that aren't the ground
// //             newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
// //         }

// //         //use rotational root motion as is
// //         newRootRotation = anim.rootRotation;

// //         //TODO Here, you could scale the difference in position and rotation to make the character go faster or slower

// //         // old way
// //         //this.transform.position = newRootPosition;
// //         //this.transform.rotation = newRootRotation;

// //         rbody.MovePosition(newRootPosition);
// //         rbody.MoveRotation(newRootRotation);


// //     }
// // }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
// [RequireComponent(typeof(CharacterInputController))]
// public class MinionRootMotionControlScript : MonoBehaviour
// {
//     private Animator anim;
//     private Rigidbody rbody;
//     private CharacterInputController cinput;

//     private int groundContactCount = 0;
//     private bool jumpRequested = false;

//     public float jumpForce = 7f; // Adjust jump strength here
//     public int maxJumpCount = 2; // Allows double jump
//     private int jumpCount = 0;

//     public bool IsGrounded => groundContactCount > 0;

//     float _inputForward = 0f;
//     float _inputTurn = 0f;

//     public float jumpableGroundNormalMaxAngle = 45f;
//     public bool closeToJumpableGround;

//     void Awake()
//     {
//         anim = GetComponent<Animator>();
//         rbody = GetComponent<Rigidbody>();
//         cinput = GetComponent<CharacterInputController>();

//         if (anim != null)
//             anim.applyRootMotion = true;
//     }

//     void Update()
//     {
//         if (cinput.enabled)
//         {
//             _inputForward = cinput.Forward;
//             _inputTurn = cinput.Turn;

//             // Detect jump input (Space key)
//             if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
//             {
//                 Debug.Log("Space pressed - jump requested");
//                 jumpRequested = true;
//             }
//         }
//     }

//     void FixedUpdate()
//     {
//         bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(transform.position, jumpableGroundNormalMaxAngle, 0.85f, 0f, out closeToJumpableGround);

//         if (isGrounded)
//         {
//             jumpCount = 0; // Reset jump count on ground
//         }

//         if (jumpRequested)
//         {
//             // Reset Y velocity for consistent jump force
//             rbody.linearVelocity = new Vector3(rbody.linearVelocity.x, 0f, rbody.linearVelocity.z);
//             rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//             jumpCount++;
//             jumpRequested = false;

//             // Trigger jump animation if parameter exists
//             if (anim != null && HasParameter(anim, "jump"))
//                 anim.SetTrigger("jump");
//         }

//         if (anim != null)
//         {
//             anim.SetFloat("velx", _inputTurn);
//             anim.SetFloat("vely", _inputForward);
//             anim.SetBool("isFalling", !isGrounded);
//         }
//     }

//     void OnCollisionEnter(Collision collision)
//     {
//         if (collision.transform.CompareTag("ground"))
//         {
//             ++groundContactCount;

//             EventManager.TriggerEvent<MinionLandsEvent, Vector3, float>(
//                 collision.contacts[0].point, collision.impulse.magnitude
//             );
//         }
//     }

//     void OnCollisionExit(Collision collision)
//     {
//         if (collision.transform.CompareTag("ground"))
//         {
//             --groundContactCount;
//         }
//     }

//     void OnAnimatorMove()
//     {
//         if (anim == null) return;

//         Vector3 newRootPosition;
//         Quaternion newRootRotation;

//         bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(transform.position, jumpableGroundNormalMaxAngle, 0.85f, 0f, out closeToJumpableGround);

//         if (isGrounded)
//         {
//             newRootPosition = anim.rootPosition;
//         }
//         else
//         {
//             newRootPosition = new Vector3(anim.rootPosition.x, transform.position.y, anim.rootPosition.z);
//         }

//         newRootRotation = anim.rootRotation;

//         rbody.MovePosition(newRootPosition);
//         rbody.MoveRotation(newRootRotation);
//     }

//     // Helper method to check if Animator has a parameter
//     bool HasParameter(Animator animator, string paramName)
//     {
//         foreach (AnimatorControllerParameter param in animator.parameters)
//         {
//             if (param.name == paramName)
//                 return true;
//         }
//         return false;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class MinionRootMotionControlScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    private int groundContactCount = 0;
    private bool jumpRequested = false;

    public float jumpForce = 7f;              // Jump strength
    public int maxJumpCount = 2;              // Double jump allowed
    private int jumpCount = 0;

    public float maxSpeed = 5f;               // Normal max speed
    public float sprintMultiplier = 2f;       // Sprint speed multiplier
    private float currentSpeed;

    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    public bool IsGrounded => groundContactCount > 0;

    float _inputForward = 0f;
    float _inputTurn = 0f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInputController>();

        if (anim != null)
            anim.applyRootMotion = true;

        currentSpeed = maxSpeed;
    }

    void Update()
    {
        if (cinput.enabled)
        {
            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;

            // Sprint check
            bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            currentSpeed = isSprinting ? maxSpeed * sprintMultiplier : maxSpeed;

            // Jump input
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
            {
                Debug.Log("Space pressed - jump requested");
                jumpRequested = true;
            }
        }
    }

    void FixedUpdate()
    {
        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(transform.position, jumpableGroundNormalMaxAngle, 0.85f, 0f, out closeToJumpableGround);

        if (isGrounded)
        {
            jumpCount = 0; // Reset jumps on ground
        }

        if (jumpRequested)
        {
            // Reset vertical velocity before jump
            Vector3 velocity = rbody.linearVelocity;
            velocity.y = 0f;
            rbody.linearVelocity = velocity;

            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            jumpRequested = false;

            if (anim != null && HasParameter(anim, "jump"))
                anim.SetTrigger("jump");
        }

        if (anim != null)
        {
            anim.SetFloat("velx", _inputTurn);
            anim.SetFloat("vely", _inputForward);
            anim.SetBool("isFalling", !isGrounded);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("ground"))
        {
            ++groundContactCount;

            EventManager.TriggerEvent<MinionLandsEvent, Vector3, float>(
                collision.contacts[0].point, collision.impulse.magnitude
            );
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("ground"))
        {
            --groundContactCount;
        }
    }

    void OnAnimatorMove()
    {
        if (anim == null) return;

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(transform.position, jumpableGroundNormalMaxAngle, 0.85f, 0f, out closeToJumpableGround);

        if (isGrounded)
        {
            // Scale root motion movement by currentSpeed / maxSpeed to apply sprint effect
            Vector3 rootMotion = anim.deltaPosition * (currentSpeed / maxSpeed);
            newRootPosition = rbody.position + rootMotion;
        }
        else
        {
            // Keep original Y when in air
            newRootPosition = new Vector3(anim.rootPosition.x, rbody.position.y, anim.rootPosition.z);
        }

        newRootRotation = anim.rootRotation;

        rbody.MovePosition(newRootPosition);
        rbody.MoveRotation(newRootRotation);
    }

    // Helper to check Animator parameter existence
    bool HasParameter(Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }
}
