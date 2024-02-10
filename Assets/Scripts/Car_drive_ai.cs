using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Car_drive_ai : Agent
{
    // References to other scripts and components
    [SerializeField] private All_checkpoint allCheckpoint;
    private Car car;
    private Restart_game restart_Game;
    public Rigidbody rBody;

    // Initialization of components
    private void Awake()
    {
        car = GetComponent<Car>(); // Retrieves the Car component attached to the same object
    }

    private void Start()
    {
        // Subscription to events related to checkpoints and collision with a wall
        allCheckpoint.OnPlayerCorrectCheckpoint += All_checkpoint_OnCarGoodCheckpoint;
        allCheckpoint.OnPlayerWrongCheckpoint += All_checkpoint_OnPlayerWrongCheckpoint;
        car.OnPlayerWall += All_checkpoint_OnPlayerWall;
        rBody = GetComponent<Rigidbody>(); // Retrieves the Rigidbody component attached to the same object
    }

    // Method called when the car passes through the correct checkpoint
    private void All_checkpoint_OnCarGoodCheckpoint(object sender, System.EventArgs e)
    {
        AddReward(1f); // Adds a reward for AI
    }

    // Method called when the car passes through the incorrect checkpoint
    private void All_checkpoint_OnPlayerWrongCheckpoint(object sender, System.EventArgs e)
    {
        Debug.Log("wrong"); // Logs a message in the console
        AddReward(-1f); // Adds a penalty for AI
    }

    // Method called when the car hits a wall
    private void All_checkpoint_OnPlayerWall(object sender, System.EventArgs e)
    {
        Debug.Log("wall"); // Logs a message in the console
        AddReward(-0.5f); // Adds a penalty for AI
    }

    // Collecting observations to be used for training AI
    public override void CollectObservations(VectorSensor sensor)
    {
        if (rBody == null)
        {
            Debug.LogError("rBody is null"); // Displays an error if rBody is not initialized
            return;
        }

        // Converts global velocity to local reference frame
        var localVelocity = transform.InverseTransformDirection(rBody.velocity);
        sensor.AddObservation(localVelocity.x); // Adds the velocity in the x-axis to observations
        sensor.AddObservation(localVelocity.y); // Adds the velocity in the y-axis to observations
        sensor.AddObservation(rBody.velocity.magnitude); // Adds the velocity magnitude to observations

        // Calculates direction to the nearest checkpoint and adds it to observations
        Vector3 checkpointForward = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;
        Vector3 directionToCheckpoint = (checkpointForward - transform.position).normalized;
        float directionDot = Vector3.Dot(transform.forward, directionToCheckpoint);
        sensor.AddObservation(directionDot); // Adds direction as an observation
    }

    // Receiving and processing actions from AI
    public override void OnActionReceived(ActionBuffers actions)
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // Interprets actions and transforms them into input for the car
        switch (actions.DiscreteActions[0])
        {
            case 0: horizontalInput = 0f; break; // No change
            case 1: horizontalInput = +1f; break; // Turn right
            case 2: horizontalInput = -1f; break; // Turn left
        }
        switch (actions.DiscreteActions[1])
        {
            case 0: verticalInput = 0f; break; // No change
            case 1: verticalInput = +1f; break; // Accelerate
            case 2: verticalInput = -1f; break; // Brake
        }

        // Passes the calculated input values to the car control function
        car.GetInput(horizontalInput, verticalInput);
    }

    // Heuristics for manual control during testing
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            discreteActionsOut[0] = 1; // index for turning right
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            discreteActionsOut[0] = 2; // index for turning left
        }
        else
        {
            discreteActionsOut[0] = 0; // index for no turn
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            discreteActionsOut[1] = 1; // index for accelerating
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            discreteActionsOut[1] = 2; // index for braking
        }
        else
        {
            discreteActionsOut[1] = 0; // index for no acceleration/braking
        }
    }
}