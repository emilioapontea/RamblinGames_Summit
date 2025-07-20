using UnityEngine;
using System.Collections;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class ThirdPersonCamera : MonoBehaviour
{
    public float positionSmoothTime = 1f;		// a public variable to adjust smoothing of camera motion
    public float rotationSmoothTime = 1f;
    public float positionMaxSpeed = 50f;        //max speed camera can move
    public float rotationMaxSpeed = 2f;
    public Transform desiredPose;			// the desired pose for the camera, specified by a transform in the game
    public Transform target;
    /// <summary>
    /// If checked, camera position will be moved closer to the target
    /// to keep the target visible if there is a collider blocking a raycast
    /// between the target and the desired camera pose.
    /// </summary>
    public bool forceVisibleTarget = false;

    protected Vector3 currentPositionCorrectionVelocity;
    //protected Vector3 currentFacingCorrectionVelocity;
    //protected float currentFacingAngleCorrVel;
    protected Quaternion quaternionDeriv;

    protected float angle;
    void Start()
    {
        if (desiredPose == null) Debug.LogError("Player camera does not have a position set");
        if (target == null) Debug.LogError("Player camera does not have a target set.");
    }

    void LateUpdate()
    {
        if (desiredPose != null)
        {
            RaycastHit hit;
            if (!forceVisibleTarget || !CheckTargetBlocked(out hit))
            {
                Debug.Log("Camera not blocked by collider, moving camera to desired pose");
                transform.position = Vector3.SmoothDamp(transform.position, desiredPose.position, ref currentPositionCorrectionVelocity, positionSmoothTime, positionMaxSpeed, Time.deltaTime);
                var targForward = desiredPose.forward;
                transform.rotation = QuaternionUtil.SmoothDamp(transform.rotation,
                    Quaternion.LookRotation(targForward, Vector3.up), ref quaternionDeriv, rotationSmoothTime);
            }
            else
            {
                Debug.Log("Camera blocked by collider, moving camera to collider position instead of desired pose");
                // If camera blocked by collider, move camera to collider position instead of desired pose
                transform.position = Vector3.SmoothDamp(transform.position, hit.point, ref currentPositionCorrectionVelocity, positionSmoothTime, positionMaxSpeed, Time.deltaTime);
                var targForward = desiredPose.forward;
                transform.rotation = QuaternionUtil.SmoothDamp(transform.rotation,
                    Quaternion.LookRotation(targForward, Vector3.up), ref quaternionDeriv, rotationSmoothTime);
            }
        }
    }

    void SpinCamera()
    {
        // Spin camera left around target while 'O' key is pressed
        if (Input.GetKey(KeyCode.O))
        {
            desiredPose.RotateAround(target.position, Vector3.up, -1 * rotationMaxSpeed);
        }
        // Spin camera right around target while 'P' key is pressed
        if (Input.GetKey(KeyCode.P))
        {
            desiredPose.RotateAround(target.position, Vector3.up, rotationMaxSpeed);
        }
    }

    bool CheckTargetBlocked(out RaycastHit hitTracker)
    {
        Vector3 TargetToCamera = desiredPose.position - target.position;
        if (Physics.Raycast(target.position, TargetToCamera, out hitTracker, TargetToCamera.magnitude))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
