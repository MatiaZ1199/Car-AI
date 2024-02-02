using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_game : MonoBehaviour
{
    // Zmienna przechowuj�ca aktualnie za�adowan� scen�
    public Scene scene;

    // Metoda Start jest wywo�ywana przy inicjalizacji skryptu
    public void Start()
    {
        // Pobiera informacje o aktualnie aktywnej scenie i zapisuje w zmiennej 'scene'
        scene = SceneManager.GetActiveScene();
    }

    // Metoda do resetowania gry
    public void Instantreset()
    {
        // Wczytuje scen� o nazwie przechowywanej w zmiennej 'scene.name', efektywnie resetuj�c gr�
        SceneManager.LoadScene(scene.name);
    }
}
