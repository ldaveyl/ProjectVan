using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCarController : MonoBehaviour
{

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizonalInput;
    private float verticalInput;
    private float currentSteeringAngle;
    private float currentBrakingForce;
    private bool isBraking;

    [SerializeField] private float motorForce, brakeForce, maxSteerAngle;


    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider, backLeftWheelCollider, backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform, backLeftWheelTransform, backRightWheelTransform;



    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }    

    private void GetInput()
    {
        horizonalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis (VERTICAL);
        isBraking = Input.GetKey("space");
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBrakingForce = isBraking ? brakeForce : 0f;

        ApplyBraking();



    }

    private void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = currentBrakingForce;
        frontRightWheelCollider.brakeTorque = currentBrakingForce;
        backLeftWheelCollider.brakeTorque = currentBrakingForce;
        backRightWheelCollider.brakeTorque = currentBrakingForce;

    }

    private void HandleSteering()
    {
        currentSteeringAngle = maxSteerAngle * horizonalInput;

        frontLeftWheelCollider.steerAngle = currentSteeringAngle;
        frontRightWheelCollider.steerAngle = currentSteeringAngle;

    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);

    }

    private void UpdateSingleWheel(WheelCollider WheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        WheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }



}
