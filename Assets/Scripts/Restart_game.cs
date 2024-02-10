using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_game : MonoBehaviour
{
    // Variable holding the currently loaded scene
    public Scene scene;

    // The Start method is called upon script initialization
    public void Start()
    {
        // Retrieves information about the currently active scene and stores it in the 'scene' variable
        scene = SceneManager.GetActiveScene();
    }

    // Method for resetting the game
    public void Instantreset()
    {
        // Loads the scene with the name stored in the variable 'scene.name', effectively resetting the game
        SceneManager.LoadScene(scene.name);
    }
}
