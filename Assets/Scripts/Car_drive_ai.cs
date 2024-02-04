using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Car_drive_ai : Agent
{
    // Referencje do innych skryptów i komponentów
    [SerializeField] private All_checkpoint allCheckpoint;
    private Car car;
    private Restart_game restart_Game;
    public Rigidbody rBody;

    // Inicjalizacja komponentów
    private void Awake()
    {
        car = GetComponent<Car>(); // Pobiera komponent Car przypisany do tego samego obiektu
    }

    private void Start()
    {
        // Subskrypcja do zdarzeñ zwi¹zanych z checkpointami i kolizj¹ z œcian¹
        allCheckpoint.OnPlayerCorrectCheckpoint += All_checkpoint_OnCarGoodCheckpoint;
        allCheckpoint.OnPlayerWrongCheckpoint += All_checkpoint_OnPlayerWrongCheckpoint;
        car.OnPlayerWall += All_checkpoint_OnPlayerWall;
        rBody = GetComponent<Rigidbody>(); // Pobiera komponent Rigidbody przypisany do tego samego obiektu
    }

    // Metoda wywo³ywana, gdy samochód przechodzi przez poprawny checkpoint
    private void All_checkpoint_OnCarGoodCheckpoint(object sender, System.EventArgs e)
    {
        AddReward(1f); // Dodaje nagrodê dla AI
    }

    // Metoda wywo³ywana, gdy samochód przechodzi przez niew³aœciwy checkpoint
    private void All_checkpoint_OnPlayerWrongCheckpoint(object sender, System.EventArgs e)
    {
        Debug.Log("wrong"); // Wyœwietla komunikat w konsoli
        AddReward(-1f); // Dodaje karê dla AI
    }

    // Metoda wywo³ywana, gdy samochód uderza w œcianê
    private void All_checkpoint_OnPlayerWall(object sender, System.EventArgs e)
    {
        Debug.Log("wall"); // Wyœwietla komunikat w konsoli
        AddReward(-0.5f); // Dodaje karê dla AI
    }

    // Zbieranie obserwacji, które bêd¹ u¿ywane do trenowania AI
    public override void CollectObservations(VectorSensor sensor)
    {
        if (rBody == null)
        {
            Debug.LogError("rBody jest null"); // Wyœwietla b³¹d, jeœli rBody nie jest zainicjowany
            return;
        }

        // Konwersja globalnej prêdkoœci do lokalnego uk³adu odniesienia
        var localVelocity = transform.InverseTransformDirection(rBody.velocity);
        sensor.AddObservation(localVelocity.x); // Dodaje prêdkoœæ w osi x do obserwacji
        sensor.AddObservation(localVelocity.y); // Dodaje prêdkoœæ w osi y do obserwacji
        sensor.AddObservation(rBody.velocity.magnitude); // Dodaje wielkoœæ prêdkoœci do obserwacji

        // Oblicza kierunek do najbli¿szego checkpointu i dodaje go do obserwacji
        Vector3 checkpointForward = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;
        Vector3 directionToCheckpoint = (checkpointForward - transform.position).normalized;
        float directionDot = Vector3.Dot(transform.forward, directionToCheckpoint);
        sensor.AddObservation(directionDot); // Dodaje kierunek jako obserwacjê
    }

    // Odbieranie i przetwarzanie akcji od AI
    public override void OnActionReceived(ActionBuffers actions)
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // Interpretuje akcje i przekszta³ca je na wejœcie dla samochodu
        switch (actions.DiscreteActions[0])
        {
            case 0: horizontalInput = 0f; break; // Brak zmian
            case 1: horizontalInput = +1f; break; // Skrêt w prawo
            case 2: horizontalInput = -1f; break; // Skrêt w lewo
        }
        switch (actions.DiscreteActions[1])
        {
            case 0: verticalInput = 0f; break; // Brak zmian
            case 1: verticalInput = +1f; break; // Przyspieszenie
            case 2: verticalInput = -1f; break; // Hamowanie
        }

        // Przekazuje obliczone wartoœci wejœciowe do funkcji steruj¹cej samochodem
        car.GetInput(horizontalInput, verticalInput);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            discreteActionsOut[0] = 1; // indeks dla skrêtu w prawo
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            discreteActionsOut[0] = 2; // indeks dla skrêtu w lewo
        }
        else
        {
            discreteActionsOut[0] = 0; // indeks dla braku skrêtu
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            discreteActionsOut[1] = 1; // indeks dla przyspieszenia
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            discreteActionsOut[1] = 2; // indeks dla hamowania
        }
        else
        {
            discreteActionsOut[1] = 0; // indeks dla braku przyspieszenia/hamowania
        }

       
    }




}


