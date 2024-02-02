using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_checkpoint : MonoBehaviour
{
    // Referencje do skrypt�w i listy checkpoint�w
    public Restart_game restart_Game;
    public List<one_checkpoint> onecheckpointlist;
    public int nextCheckpointindex;
    public int Score;
    // Zdarzenia wywo�ywane przy poprawnym i b��dnym przejechaniu przez checkpoint
    public event EventHandler OnPlayerWrongCheckpoint;
    public event EventHandler OnPlayerCorrectCheckpoint;

    // Metoda Awake inicjalizuje list� checkpoint�w
    public void Awake()
    {
        // Szuka obiektu "Checkpoint" w hierarchii i tworzy list� checkpoint�w
        Transform checkpointTransform = transform.Find("Checkpoint");
        onecheckpointlist = new List<one_checkpoint>();
        foreach (Transform checkpointoneTransofrom in checkpointTransform)
        {
            // Dla ka�dego dziecka obiektu "Checkpoint" pobiera komponent one_checkpoint i dodaje do listy
            one_checkpoint one_Checkpoint = checkpointoneTransofrom.GetComponent<one_checkpoint>();
            one_Checkpoint.SetAllCheckpoint(this);
            onecheckpointlist.Add(one_Checkpoint);
        }
    }

    // Metoda wywo�ywana, gdy gracz przejedzie przez checkpoint
    public void ThroughtGoodCheckpoint(one_checkpoint one_Checkpoint)
    {
        // Sprawdza, czy przejechany checkpoint jest nast�pnym w kolejno�ci
        if (onecheckpointlist.IndexOf(one_Checkpoint) == nextCheckpointindex)
        {
            // Aktualizuje indeks nast�pnego checkpointu i zwi�ksza wynik
            nextCheckpointindex = (nextCheckpointindex + 1) % onecheckpointlist.Count;
            Score = Score + 1;
            // Wywo�uje zdarzenie poprawnego checkpointu
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Wywo�uje zdarzenie b��dnego checkpointu i resetuje gr�
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
            restart_Game.Instantreset();
        }
    }
}
