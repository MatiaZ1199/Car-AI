using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    // Zdarzenie wywo�ywane, gdy samoch�d uderzy w �cian�
    public event EventHandler OnPlayerWall;
    public Restart_game restart_Game;

    // Wej�cia steruj�ce samochodem
    public float horizontalInput;
    public float verticalInput;
    private float steerAngle;

    // Ko�a samochodu
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    // Transformacje ko�a, aby umo�liwi� ich wizualn� aktualizacj�
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    // Maksymalny k�t skr�tu i si�a nap�dowa
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    private void FixedUpdate()
    {
        // Obs�uga ruchu i sterowania samochodu
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // Odbiera wej�cie od gracza lub AI i dodaje do bie��cego wej�cia
    public void GetInput(float horizontalInput, float verticalInput)
    {
        this.horizontalInput = horizontalInput; //+ Input.GetAxis("Horizontal");
        this.verticalInput = verticalInput; //+ Input.GetAxis("Vertical");
    }

    // Obs�uguje skr�canie ko�ami przednimi
    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    // Obs�uguje nap�d ko�ami przednimi
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    // Aktualizuje po�o�enie i rotacj� ko�a na podstawie kolidera ko�a
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

    // Wywo�ywane, gdy samoch�d koliduje z obiektem oznaczonym tagiem "Respawn"
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            // Informuje o zdarzeniu uderzenia w �cian�
            OnPlayerWall?.Invoke(this, EventArgs.Empty);
        }
    }
    void Update()
    {
        // Sprawd� rotacj� pojazdu na osi Z
        float zRotation = transform.eulerAngles.z;

        // Je�li rotacja Z jest r�wna 90, -90 lub 180 stopni, zresetuj gr�
        if (zRotation >= 90 && zRotation <= 270)
        {
            restart_Game.Instantreset();
        }
    }
}   
