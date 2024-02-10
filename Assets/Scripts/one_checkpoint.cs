using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class one_checkpoint : MonoBehaviour
{
    // Variable for storing a reference to the main script managing checkpoints.
    private All_checkpoint all_Checkpoint;

    // Method called when some collider enters the area of this collider set as a trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object that collided has the tag "Player".
        if (other.CompareTag("Player"))
        {
            // Informs the main checkpoint script that the player has passed through this checkpoint.
            all_Checkpoint.ThroughtGoodCheckpoint(this);
        }
        else
        {
            // If it's not the player, do nothing and exit the function.
            return;
        }


    }

    // Method for setting the reference to the main checkpoints script.
    public void SetAllCheckpoint(All_checkpoint all_Checkpoint)
    {
        // Assigns the reference to the passed all_Checkpoint object.
        this.all_Checkpoint = all_Checkpoint;
    }
}
