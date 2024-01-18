using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_checkpoint : MonoBehaviour
{
    public Restart_game restart_Game;
    private List<one_checkpoint> onecheckpointlist;
    private int nextCheckpointindex;
    public int Score;
    public event EventHandler OnPlayerWrongCheckpoint;
    public event EventHandler OnPlayerCorrectCheckpoint;
    


    private void Awake()
    {

        Transform checkpointTransform = transform.Find("Checkpoint");
        onecheckpointlist = new List<one_checkpoint>();
        foreach (Transform checkpointoneTransofrom in checkpointTransform)
        {
            one_checkpoint one_Checkpoint = checkpointoneTransofrom.GetComponent<one_checkpoint>();
            one_Checkpoint.SetAllCheckpoint(this);
            onecheckpointlist.Add(one_Checkpoint);
        }
    }

    public void ThroughtGoodCheckpoint(one_checkpoint one_Checkpoint)
    {
        if (onecheckpointlist.IndexOf(one_Checkpoint) == nextCheckpointindex)
        {
            nextCheckpointindex = (nextCheckpointindex + 1) % onecheckpointlist.Count;
            Score = Score + 1;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Wrong");
            restart_Game.Instantreset();
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }

}