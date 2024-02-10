using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    // Event triggered when the car hits a wall
    public event EventHandler OnPlayerWall;
    public Restart_game restart_Game;

    // Car control inputs
    public float horizontalInput;
    public float verticalInput;
    private float steerAngle;

    // Car wheel colliders
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    // Wheel transforms for visual updates
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    // Maximum steering angle and motor force
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    private void FixedUpdate()
    {
        // Handling car movement and steering
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // Receives input from the player or AI and applies it
    public void GetInput(float horizontalInput, float verticalInput)
    {
        this.horizontalInput = horizontalInput; //+ Input.GetAxis("Horizontal");
        this.verticalInput = verticalInput; //+ Input.GetAxis("Vertical");
    }

    // Handles the steering of the front wheels
    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    // Handles the driving force of the front wheels
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    // Updates the position and rotation of the wheels based on the wheel collider
    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    // Updates a single wheel's position and rotation
    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    // Triggered when the car collides with an object tagged "Respawn"
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            // Notifies about the wall hit event
            restart_Game.Instantreset();
            OnPlayerWall?.Invoke(this, EventArgs.Empty);
        }
    }

    void Update()
    {
        // Check the vehicle's rotation on the Z axis
        float zRotation = transform.eulerAngles.z;

        // If Z rotation is equal to 90, -90, or 180 degrees, reset the game
        if (zRotation >= 90 && zRotation <= 270)
        {
            restart_Game.Instantreset();
        }
    }
}
