using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    [Header("Constants")]
    public Rigidbody rb;
    //unity controls and constants input
    public float AccelerationMod;
    public float maxRotationSpeed = 500.0f; // Maximum rotation speed
    public float acceleration = 100.0f; // Acceleration rate
    private float currentRotationSpeed = 0.0f; // Current rotation speed
    public float DecelerationMod;
    public bool autoLockCursor;
    public float MaximumMovementSpeed = 100f;

    [Header("Controls")]

    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;

    private Vector3 _moveSpeed;
    private void Start()
    {
        _moveSpeed = Vector3.zero;
        Cursor.lockState = (autoLockCursor) ? CursorLockMode.Locked : CursorLockMode.None;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {

        var acceleration = HandleKeyInput() * Time.deltaTime;

        _moveSpeed += acceleration;

        HandleDeceleration(acceleration);

        // clamp the move speed
        if (_moveSpeed.magnitude > MaximumMovementSpeed)
        {
            _moveSpeed = _moveSpeed.normalized * MaximumMovementSpeed;
        }

        transform.Translate(_moveSpeed * Time.deltaTime);
    }
    private Vector3 HandleKeyInput()
    {
        var acceleration = Vector3.zero;

        // Key input detection
        if (Input.GetKey(Forwards))
        {
            acceleration.z += 2;
        }

        if (Input.GetKey(Backwards))
        {
            acceleration.z -= 1;
        }

        // Adjust rotation speed based on input
        if (Input.GetKey(Left) && (Input.GetKey(Forwards) || Input.GetKey(Backwards)))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -maxRotationSpeed);

        }
        
        if (Input.GetKey(Right) && (Input.GetKey(Forwards) || Input.GetKey(Backwards)))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * maxRotationSpeed);
        }

        return acceleration.normalized * AccelerationMod;
    }

    private void HandleDeceleration(Vector3 acceleration)
    {
        //deceleration functionality
        if (Mathf.Approximately(Mathf.Abs(acceleration.x), 0))
        {
            if (Mathf.Abs(_moveSpeed.x) < DecelerationMod * Time.deltaTime)
            {
                _moveSpeed.x = 0;
            }
            else
            {
                _moveSpeed.x -= DecelerationMod * Mathf.Sign(_moveSpeed.x) * Time.deltaTime;
            }
        }

        if (Mathf.Approximately(Mathf.Abs(acceleration.z), 0))
        {
            if (Mathf.Abs(_moveSpeed.z) < DecelerationMod * Time.deltaTime)
            {
                _moveSpeed.z = 0;
            }
            else
            {
                _moveSpeed.z -= DecelerationMod * Mathf.Sign(_moveSpeed.z) * Time.deltaTime;
            }
        }
    }
}
