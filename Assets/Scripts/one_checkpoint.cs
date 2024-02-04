using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class one_checkpoint : MonoBehaviour
{
    // Zmienna do przechowywania referencji do g³ównego skryptu zarz¹dzaj¹cego checkpointami.
    private All_checkpoint all_Checkpoint;

    // Metoda wywo³ywana, gdy jakiœ collider wchodzi w obszar tego collidera ustawionego jako trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdza, czy obiekt, który wszed³ w kolizjê, ma tag "Player".
        if (other.CompareTag("Player"))
        {
            // Informuje g³ówny skrypt checkpointów, ¿e gracz przeszed³ przez ten checkpoint.
            all_Checkpoint.ThroughtGoodCheckpoint(this);
        }
        else
        {
                // Jeœli to nie jest gracz, nie rób nic i wyjdŸ z funkcji.
                return;
        }
        
      
    }

    // Metoda umo¿liwiaj¹ca ustawienie referencji do g³ównego skryptu checkpointów.
    public void SetAllCheckpoint(All_checkpoint all_Checkpoint)
    {
        // Przypisuje referencjê do przekazanego obiektu all_Checkpoint.
        this.all_Checkpoint = all_Checkpoint;
    }
}

