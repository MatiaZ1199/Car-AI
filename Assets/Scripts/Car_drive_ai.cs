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

    public int nextCheckpoint = 1;

    private Car car;
    


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
        AddReward(-1f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = allCheckpoint.nextCheckpointindex(transform).transform.forward;
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




   /* public override void Heuristic(in ActionBuffers actionsOut)
    {
        int horizontaAction = 0;
        if (Input.GetKey(KeyCode.UpArrow)) horizontaAction = 1;
        if (Input.GetKey(KeyCode.DownArrow)) horizontaAction = 2;

        int verticalAction = 0;
        if (Input.GetKey(KeyCode.RightArrow)) verticalAction = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) verticalAction = 2;


        ActionSegment<int> discretAction = actionsOut.DiscreteActions;
        discretAction[0] = horizontaAction;
        discretAction[0] = verticalAction;
    }*/

}

