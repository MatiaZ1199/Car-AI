using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_game : MonoBehaviour
{
    // Zmienna przechowuj¹ca aktualnie za³adowan¹ scenê
    public Scene scene;

    // Metoda Start jest wywo³ywana przy inicjalizacji skryptu
    public void Start()
    {
        // Pobiera informacje o aktualnie aktywnej scenie i zapisuje w zmiennej 'scene'
        scene = SceneManager.GetActiveScene();
    }

    // Metoda do resetowania gry
    public void Instantreset()
    {
        // Wczytuje scenê o nazwie przechowywanej w zmiennej 'scene.name', efektywnie resetuj¹c grê
        SceneManager.LoadScene(scene.name);
    }
}
