using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Constants")]

    //unity controls and constants input
    public float AccelerationMod;
    public float XAxisSensitivity;
    public float YAxisSensitivity;
    public float DecelerationMod;
    public bool autoLockCursor = true;

    public float SprintModifier;

    [Space]

    [Range(0, 89)] public float MaxXAngle = 89f;

    [Space]

    public float MaximumMovementSpeed = 100f;

    [Header("Controls")]

    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Up = KeyCode.Space;
    public KeyCode Down = KeyCode.LeftShift;
    public KeyCode Sprint = KeyCode.LeftControl;

    private Vector3 _moveSpeed;


    private void Start()
    {
        _moveSpeed = Vector3.zero;
        Cursor.lockState = (autoLockCursor) ? CursorLockMode.Locked : CursorLockMode.None;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMouseRotation();

        var acceleration = HandleKeyInput() * Time.deltaTime;

        _moveSpeed += acceleration;

        HandleDeceleration(acceleration);

        // clamp the move speed
        if (_moveSpeed.magnitude > MaximumMovementSpeed)
        {
            float sprintModifier = Input.GetKey(Sprint) ? SprintModifier : 1.0f;

            _moveSpeed = _moveSpeed.normalized * MaximumMovementSpeed * sprintModifier;
        }

        transform.Translate(_moveSpeed * Time.deltaTime);
    }
    private Vector3 HandleKeyInput()
    {
        var acceleration = Vector3.zero;

        // Key input detection
        if (Input.GetKey(Forwards))
        {
            acceleration.z += 1;
        }

        if (Input.GetKey(Backwards))
        {
            acceleration.z -= 1;
        }

        if (Input.GetKey(Left))
        {
            acceleration.x -= 1;
        }

        if (Input.GetKey(Right))
        {
            acceleration.x += 1;
        }

        if (Input.GetKey(Up))
        {
            acceleration.y += 1;
        }

        if (Input.GetKey(Down))
        {
            acceleration.y -= 1;
        }

        return acceleration.normalized * AccelerationMod;
    }

    private float _rotationX;

    private void HandleMouseRotation()
    {
        //mouse input
        var rotationHorizontal = XAxisSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime;
        var rotationVertical = YAxisSensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime;

        //applying mouse rotation
        // always rotate Y in global world space to avoid gimbal lock
        transform.Rotate(Vector3.up * rotationHorizontal, Space.World);

        var rotationY = transform.localEulerAngles.y;

        _rotationX += rotationVertical;
        _rotationX = Mathf.Clamp(_rotationX, -MaxXAngle, MaxXAngle);

        transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
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

        if (Mathf.Approximately(Mathf.Abs(acceleration.y), 0))
        {
            if (Mathf.Abs(_moveSpeed.y) < DecelerationMod * Time.deltaTime)
            {
                _moveSpeed.y = 0;
            }
            else
            {
                _moveSpeed.y -= DecelerationMod * Mathf.Sign(_moveSpeed.y) * Time.deltaTime;
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
