using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class one_checkpoint : MonoBehaviour
{
    
    private All_checkpoint all_Checkpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            all_Checkpoint.ThroughtCheckpoint(this);
        }
    }
    public void SetAllCheckpoint(All_checkpoint all_Checkpoint)
    {
        this.all_Checkpoint = all_Checkpoint;
    }
}
