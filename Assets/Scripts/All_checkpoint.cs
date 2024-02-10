using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_checkpoint : MonoBehaviour
{
    // References to scripts and the list of checkpoints
    public Restart_game restart_Game;
    public List<one_checkpoint> onecheckpointlist;
    public int nextCheckpointindex;
    public int Score;
    // Events triggered upon passing through a checkpoint correctly or incorrectly
    public event EventHandler OnPlayerWrongCheckpoint;
    public event EventHandler OnPlayerCorrectCheckpoint;

    // The Awake method initializes the list of checkpoints
    public void Awake()
    {
        // Searches for an object named "Checkpoint" in the hierarchy and creates a list of checkpoints
        Transform checkpointTransform = transform.Find("Checkpoint");
        onecheckpointlist = new List<one_checkpoint>();
        foreach (Transform checkpointoneTransform in checkpointTransform)
        {
            // For each child of the "Checkpoint" object, gets the one_checkpoint component and adds it to the list
            one_checkpoint one_Checkpoint = checkpointoneTransform.GetComponent<one_checkpoint>();
            one_Checkpoint.SetAllCheckpoint(this);
            onecheckpointlist.Add(one_Checkpoint);
        }
    }

    // Method called when a player passes through a checkpoint
    public void ThroughtGoodCheckpoint(one_checkpoint one_Checkpoint)
    {
        // Checks if the passed checkpoint is the next in order
        if (onecheckpointlist.IndexOf(one_Checkpoint) == nextCheckpointindex)
        {
            // Updates the index of the next checkpoint and increases the score
            nextCheckpointindex = (nextCheckpointindex + 1) % onecheckpointlist.Count;
            Score += 1;
            // Triggers the correct checkpoint event
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Triggers the wrong checkpoint event and resets the game
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
            restart_Game.Instantreset();
        }
    }
}
