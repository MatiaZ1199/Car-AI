using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Car_drive_ai : Agent
{


    [SerializeField] private All_checkpoint allCheckpoint;
    [SerializeField] private Transform spawnPoint;



    private Car car;
    private Restart_game restart_Game;


    private void Awake()
    {
        car = GetComponent<Car>();
    }

    private void Start()
    {
        allCheckpoint.OnPlayerCorrectCheckpoint += All_checkpoint_OnCarGoodCheckpoint;
        allCheckpoint.OnPlayerWrongCheckpoint += All_checkpoint_OnPlayerWrongCheckpoint;
    }

  
    private void All_checkpoint_OnCarGoodCheckpoint(object sender, System.EventArgs e)
    {
        AddReward(1f);
    }

    private void All_checkpoint_OnPlayerWrongCheckpoint(object sender, System.EventArgs e)
    {
        Debug.Log("wrong");
        AddReward(-1f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            AddReward(-0.1f);
            Debug.Log("Wall");
            return;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = allCheckpoint.nextCheckpointindex(transform)
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        sensor.AddObservation(directionDot);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        switch (actions.DiscreteActions[0])
        {
            case 0: horizontalInput = 0f; break;
            case 1: horizontalInput = +1f; break;
            case 2: horizontalInput = -1f; break;
        }
        switch (actions.DiscreteActions[1])
        {
            case 0: verticalInput = 0f; break;
            case 1: verticalInput = +1f; break;
            case 2: verticalInput = -1f; break;
        }


        car.GetInput(horizontalInput, verticalInput);
    }

   








}

