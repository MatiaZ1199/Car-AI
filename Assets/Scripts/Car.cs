using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    // Zdarzenie wywo³ywane, gdy samochód uderzy w œcianê
    public event EventHandler OnPlayerWall;
    public Restart_game restart_Game;

    // Wejœcia steruj¹ce samochodem
    public float horizontalInput;
    public float verticalInput;
    private float steerAngle;

    // Ko³a samochodu
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    // Transformacje ko³a, aby umo¿liwiæ ich wizualn¹ aktualizacjê
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    // Maksymalny k¹t skrêtu i si³a napêdowa
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    private void FixedUpdate()
    {
        // Obs³uga ruchu i sterowania samochodu
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // Odbiera wejœcie od gracza lub AI i dodaje do bie¿¹cego wejœcia
    public void GetInput(float horizontalInput, float verticalInput)
    {
        this.horizontalInput = horizontalInput; //+ Input.GetAxis("Horizontal");
        this.verticalInput = verticalInput; //+ Input.GetAxis("Vertical");
    }

    // Obs³uguje skrêcanie ko³ami przednimi
    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    // Obs³uguje napêd ko³ami przednimi
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    // Aktualizuje po³o¿enie i rotacjê ko³a na podstawie kolidera ko³a
    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    // Wywo³ywane, gdy samochód koliduje z obiektem oznaczonym tagiem "Respawn"
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            // Informuje o zdarzeniu uderzenia w œcianê
            OnPlayerWall?.Invoke(this, EventArgs.Empty);
        }
    }
    void Update()
    {
        // SprawdŸ rotacjê pojazdu na osi Z
        float zRotation = transform.eulerAngles.z;

        // Jeœli rotacja Z jest równa 90, -90 lub 180 stopni, zresetuj grê
        if (zRotation >= 90 && zRotation <= 270)
        {
            restart_Game.Instantreset();
        }
    }
}   
