using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public All_checkpoint all_Checkpoint;
    public Transform player;
    public Text text;
    
    void Update()
    {
        text.text = all_Checkpoint.Score.ToString();    
    }
}
