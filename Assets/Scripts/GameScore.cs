using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public All_checkpoint all_Checkpoint;

    // Start is called before the first frame update
    public Transform player;
    public Text text;
    // Update is called once per frame
    void Update()
    {
        text.text = all_Checkpoint.Score.ToString();    
    }
}
