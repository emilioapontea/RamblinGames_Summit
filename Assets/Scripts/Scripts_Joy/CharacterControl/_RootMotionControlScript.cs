﻿/*

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

////require some things the bot control needs
//[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
//[RequireComponent(typeof(CharacterInputController))]
//public class RootMotionControlScript : MonoBehaviour
//{
//    private Animator anim;	
//    private Rigidbody rbody;
//    private CharacterInputController cinput;

//    private Transform leftFoot;
//    private Transform rightFoot;


//    public GameObject buttonPressStandingSpot;
//    public float buttonCloseEnoughForMatchDistance = 2f;
//    public float buttonCloseEnoughForPressDistance = 0.22f;
//    public float buttonCloseEnoughForPressAngleDegrees = 5f;
//    public float initalMatchTargetsAnimTime = 0.25f;
//    public float exitMatchTargetsAnimTime = 0.75f;


//    // classic input system only polls in Update()
//    // so must treat input events like discrete button presses as
//    // "triggered" until consumed by FixedUpdate()...
//    bool _inputActionFired = false;

//    // ...however constant input measures like axes can just have most recent value
//    // cached.
//    float _inputForward = 0f;
//    float _inputTurn = 0f;


//    //Useful if you implement jump in the future...
//    public float jumpableGroundNormalMaxAngle = 45f;
//    public bool closeToJumpableGround;


//    private int groundContactCount = 0;

//    public bool IsGrounded
//    {
//        get
//        {
//            return groundContactCount > 0;
//        }
//    }

//    void Awake()
//    {

//        anim = GetComponent<Animator>();

//        if (anim == null)
//            Debug.Log("Animator could not be found");

//        rbody = GetComponent<Rigidbody>();

//        if (rbody == null)
//            Debug.Log("Rigid body could not be found");

//        cinput = GetComponent<CharacterInputController>();
//        if (cinput == null)
//            Debug.Log("CharacterInput could not be found");
//    }


//    // Use this for initialization
//    void Start()
//    {
//		//example of how to get access to certain limbs
//        leftFoot = this.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
//        rightFoot = this.transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot");

//        if (leftFoot == null || rightFoot == null)
//            Debug.Log("One of the feet could not be found");

//    }


//    private void Update()
//    {
//        if (cinput.enabled)
//        {
//            _inputForward = cinput.Forward;
//            _inputTurn = cinput.Turn;

//            // Note that we don't overwrite a true value already stored
//            // Is only cleared to false in FixedUpdate()
//            // This makes certain that the action is handled!
//            _inputActionFired = _inputActionFired || cinput.Action;

//        }
//    }


//    void FixedUpdate()
//    {

//        bool doButtonPress = false;
//        bool doMatchToButtonPress = false;

//        //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
//        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
//        //Therefore, an additional raycast approach is used to check for close ground.
//        //This is good for allowing player to jump and not be frustrated that the jump button doesn't
//        //work
//        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

//        float buttonDistance = float.MaxValue;
//        float buttonAngleDegrees = float.MaxValue;

//        if (buttonPressStandingSpot != null)
//        {
//            buttonDistance = Vector3.Distance(transform.position, buttonPressStandingSpot.transform.position);
//            buttonAngleDegrees = Quaternion.Angle(transform.rotation, buttonPressStandingSpot.transform.rotation);
//        }

//        if(_inputActionFired)
//        {
//            _inputActionFired = false; // clear the input event that came from Update()

//            Debug.Log("Action pressed");

//            if (buttonDistance <= buttonCloseEnoughForMatchDistance)
//            {
//                if(buttonDistance <= buttonCloseEnoughForPressDistance &&
//                    buttonAngleDegrees <= buttonCloseEnoughForPressAngleDegrees)
//                {
//                    Debug.Log("Button press initiated");

//                    doButtonPress = true;

//                }
//                else
//                {
//                    // TODO UNCOMMENT THESE LINES FOR TARGET MATCHING
//                    // Debug.Log("match to button initiated");
//                    // doMatchToButtonPress = true;
//                }

//            }
//        }


//        // TODO HANDLE BUTTON MATCH TARGET HERE



//        anim.SetFloat("velx", _inputTurn);
//        anim.SetFloat("vely", _inputForward);
//        anim.SetBool("isFalling", !isGrounded);
//        anim.SetBool("doButtonPress", doButtonPress);
//        anim.SetBool("matchToButtonPress", doMatchToButtonPress);

//    }


//    //This is a physics callback
//    void OnCollisionEnter(Collision collision)
//    {

//        if (collision.transform.gameObject.tag == "ground")
//        {

//            ++groundContactCount;

//            // Generate an event that might play a sound, generate a particle effect, etc.
//            EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);

//        }

//    }

//    private void OnCollisionExit(Collision collision)
//    {

//        if (collision.transform.gameObject.tag == "ground")
//        {
//            --groundContactCount;
//        }

//    }

//    void OnAnimatorMove()
//    {

//        Vector3 newRootPosition;
//        Quaternion newRootRotation;

//        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

//        if (isGrounded)
//        {
//         	//use root motion as is if on the ground		
//            newRootPosition = anim.rootPosition;        
//        }
//        else
//        {
//            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
//            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
//        }

//        //use rotational root motion as is
//        newRootRotation = anim.rootRotation;

//        //TODO Here, you could scale the difference in position and rotation to make the character go faster or slower

//        // old way
//        //this.transform.position = newRootPosition;
//        //this.transform.rotation = newRootRotation;

//        rbody.MovePosition(newRootPosition);
//        rbody.MoveRotation(newRootRotation);
//    }




//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


//require some things the bot control needs
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class RootMotionControlScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    private Transform leftFoot;
    private Transform rightFoot;


    public GameObject buttonPressStandingSpot;
    public float buttonCloseEnoughForMatchDistance = 2f;
    public float buttonCloseEnoughForPressDistance = 0.22f;
    public float buttonCloseEnoughForPressAngleDegrees = 5f;
    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;

    //changing 1 1 1

    public GameObject buttonObject;


    public float animationSpeed = 1f;      // Controls Animator speed
    public float rootMovementSpeed = 1f;   // Scales positional root motion
    public float rootTurnSpeed = 1f;       // Scales rotational root motion



    // classic input system only polls in Update()
    // so must treat input events like discrete button presses as
    // "triggered" until consumed by FixedUpdate()...
    bool _inputActionFired = false;

    // ...however constant input measures like axes can just have most recent value
    // cached.
    float _inputForward = 0f;
    float _inputTurn = 0f;


    //Useful if you implement jump in the future...
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;


    private int groundContactCount = 0;


    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    void Awake()
    {

        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }


    // Use this for initialization
    void Start()
    {
        //example of how to get access to certain limbs
        leftFoot = this.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
        rightFoot = this.transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");

    }


    private void Update()
    {
        if (cinput.enabled)
        {
            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;

            // Note that we don't overwrite a true value already stored
            // Is only cleared to false in FixedUpdate()
            // This makes certain that the action is handled!
            _inputActionFired = _inputActionFired || cinput.Action;

        }
    }


    void FixedUpdate()
    {

        bool doButtonPress = false;
        bool doMatchToButtonPress = false;

        //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
        //Therefore, an additional raycast approach is used to check for close ground.
        //This is good for allowing player to jump and not be frustrated that the jump button doesn't
        //work
        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        float buttonDistance = float.MaxValue;
        float buttonAngleDegrees = float.MaxValue;

        if (buttonPressStandingSpot != null)
        {
            buttonDistance = Vector3.Distance(transform.position, buttonPressStandingSpot.transform.position);
            buttonAngleDegrees = Quaternion.Angle(transform.rotation, buttonPressStandingSpot.transform.rotation);
        }

        if (_inputActionFired)
        {
            _inputActionFired = false; // clear the input event that came from Update()

            Debug.Log("Action pressed");

            if (buttonDistance <= buttonCloseEnoughForMatchDistance)
            {
                if (buttonDistance <= buttonCloseEnoughForPressDistance &&
                    buttonAngleDegrees <= buttonCloseEnoughForPressAngleDegrees)
                {
                    Debug.Log("Button press initiated");

                    doButtonPress = true;

                }
                else
                {
                    // TODO UNCOMMENT THESE LINES FOR TARGET MATCHING
                    Debug.Log("match to button initiated");
                    doMatchToButtonPress = true;
                }

            }
        }


        // TODO HANDLE BUTTON MATCH TARGET HERE

        // get info about current animation
        var animState = anim.GetCurrentAnimatorStateInfo(0);
        // If the transition to button press has been initiated then we want
        // to correct the character position to the correct place
        if (animState.IsName("MatchToButtonPress")
            && !anim.IsInTransition(0) && !anim.isMatchingTarget)
        {
            if (buttonPressStandingSpot != null)
            {
                Debug.Log("Target matching correction started");
                initalMatchTargetsAnimTime = animState.normalizedTime;
                var t = buttonPressStandingSpot.transform;

                anim.MatchTarget(
                    t.position,
                    t.rotation,
                    AvatarTarget.Root,
                    new MatchTargetWeightMask(new Vector3(1f, 0f, 1f), 1f),
                    initalMatchTargetsAnimTime,
                    exitMatchTargetsAnimTime
                );
            }
        }




        anim.SetFloat("velx", _inputTurn);
        anim.SetFloat("vely", _inputForward);
        anim.SetBool("isFalling", !isGrounded);
        anim.SetBool("doButtonPress", doButtonPress);
        anim.SetBool("matchToButtonPress", doMatchToButtonPress);

        // Adjust overall Animator playback speed
        anim.speed = animationSpeed;


    }


    //This is a physics callback
    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {

            ++groundContactCount;

            // Generate an event that might play a sound, generate a particle effect, etc.
            EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);

        }

    }

    //add
    void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
            if (astate.IsName("ButtonPress"))
            {
                float buttonWeight = anim.GetFloat("buttonClose");
                if (buttonObject != null)
                {
                    anim.SetLookAtWeight(buttonWeight);
                    anim.SetLookAtPosition(buttonObject.transform.position);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, buttonWeight);
                    anim.SetIKPosition(AvatarIKGoal.RightHand, buttonObject.transform.position);
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }

    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        if (isGrounded)
        {
            //use root motion as is if on the ground        
            newRootPosition = anim.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }

        //use rotational root motion as is
        newRootRotation = anim.rootRotation;

        //TODO Here, you could scale the difference in position and rotation to make the character go faster or slower

        // old way
        //this.transform.position = newRootPosition;
        //this.transform.rotation = newRootRotation;

        // rbody.MovePosition(newRootPosition);
        // rbody.MoveRotation(newRootRotation);

        // Compute delta motion
        Vector3 deltaPosition = newRootPosition - transform.position;
        Quaternion deltaRotation = Quaternion.Inverse(transform.rotation) * newRootRotation;

        // // Scale the deltas
        deltaPosition *= rootMovementSpeed;
        deltaRotation = Quaternion.Slerp(Quaternion.identity, deltaRotation, rootTurnSpeed);

        // // Apply scaled motion
        rbody.MovePosition(transform.position + deltaPosition);
        rbody.MoveRotation(transform.rotation * deltaRotation);

        // Scale deltas using LerpUnclamped
        // Vector3 scaledDeltaPosition = Vector3.LerpUnclamped(Vector3.zero, deltaPosition, rootMovementSpeed);
        // Quaternion scaledDeltaRotation = Quaternion.LerpUnclamped(Quaternion.identity, deltaRotation, rootTurnSpeed);

        // Apply the scaled motion
        // rbody.MovePosition(transform.position + scaledDeltaPosition);
        // rbody.MoveRotation(transform.rotation * scaledDeltaRotation);

    }




}

*/