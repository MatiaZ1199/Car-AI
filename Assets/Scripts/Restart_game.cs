using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart_game : MonoBehaviour
{
    public Scene scene;
    public void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void Instantreset()
    {
        SceneManager.LoadScene(scene.name);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scene.name);
            
        }
    }
}   