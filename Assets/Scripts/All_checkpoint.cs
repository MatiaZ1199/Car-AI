using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_checkpoint : MonoBehaviour
{
    // Referencje do skryptów i listy checkpointów
    public Restart_game restart_Game;
    public List<one_checkpoint> onecheckpointlist;
    public int nextCheckpointindex;
    public int Score;
    // Zdarzenia wywo³ywane przy poprawnym i b³êdnym przejechaniu przez checkpoint
    public event EventHandler OnPlayerWrongCheckpoint;
    public event EventHandler OnPlayerCorrectCheckpoint;

    // Metoda Awake inicjalizuje listê checkpointów
    public void Awake()
    {
        // Szuka obiektu "Checkpoint" w hierarchii i tworzy listê checkpointów
        Transform checkpointTransform = transform.Find("Checkpoint");
        onecheckpointlist = new List<one_checkpoint>();
        foreach (Transform checkpointoneTransofrom in checkpointTransform)
        {
            // Dla ka¿dego dziecka obiektu "Checkpoint" pobiera komponent one_checkpoint i dodaje do listy
            one_checkpoint one_Checkpoint = checkpointoneTransofrom.GetComponent<one_checkpoint>();
            one_Checkpoint.SetAllCheckpoint(this);
            onecheckpointlist.Add(one_Checkpoint);
        }
    }

    // Metoda wywo³ywana, gdy gracz przejedzie przez checkpoint
    public void ThroughtGoodCheckpoint(one_checkpoint one_Checkpoint)
    {
        // Sprawdza, czy przejechany checkpoint jest nastêpnym w kolejnoœci
        if (onecheckpointlist.IndexOf(one_Checkpoint) == nextCheckpointindex)
        {
            // Aktualizuje indeks nastêpnego checkpointu i zwiêksza wynik
            nextCheckpointindex = (nextCheckpointindex + 1) % onecheckpointlist.Count;
            Score = Score + 1;
            // Wywo³uje zdarzenie poprawnego checkpointu
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Wywo³uje zdarzenie b³êdnego checkpointu i resetuje grê
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
            restart_Game.Instantreset();
        }
    }
}
