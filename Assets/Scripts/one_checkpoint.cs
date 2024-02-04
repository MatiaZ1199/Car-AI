using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class one_checkpoint : MonoBehaviour
{
    // Zmienna do przechowywania referencji do g��wnego skryptu zarz�dzaj�cego checkpointami.
    private All_checkpoint all_Checkpoint;

    // Metoda wywo�ywana, gdy jaki� collider wchodzi w obszar tego collidera ustawionego jako trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdza, czy obiekt, kt�ry wszed� w kolizj�, ma tag "Player".
        if (other.CompareTag("Player"))
        {
            // Informuje g��wny skrypt checkpoint�w, �e gracz przeszed� przez ten checkpoint.
            all_Checkpoint.ThroughtGoodCheckpoint(this);
        }
        else
        {
                // Je�li to nie jest gracz, nie r�b nic i wyjd� z funkcji.
                return;
        }
        
      
    }

    // Metoda umo�liwiaj�ca ustawienie referencji do g��wnego skryptu checkpoint�w.
    public void SetAllCheckpoint(All_checkpoint all_Checkpoint)
    {
        // Przypisuje referencj� do przekazanego obiektu all_Checkpoint.
        this.all_Checkpoint = all_Checkpoint;
    }
}

