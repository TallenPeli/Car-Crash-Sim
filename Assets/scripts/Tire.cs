using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tire : MonoBehaviour
{
    public Rigidbody rb; // rigid body of the tire
    public char gear; // gear control
    public float acceleration = 100f; // how much you accelerate when the gas activates
    public float breakForce = 0.05f; // this is the break force for when the break activates
    public float parkBreakForce = 1000f; // break force when you park
    public float steerAmount = 0f; // use this to control how much the wheel steers
    public float targetAngle = 90f; // Target angle in degrees

    public float TurnSpeed = 10f;

    public KeyCode Accelerate = KeyCode.W;
    public KeyCode Break = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;

    private void turn(){
        if (Input.GetKey(Left)){
            // Calculate the rotation needed for the Y-axis
            Quaternion yRotation = Quaternion.Euler(0, TurnSpeed * Time.deltaTime, 0);

            // Apply the rotation to the object
            transform.rotation *= yRotation;
        }
        if (Input.GetKey(Right)){
            // Calculate the rotation needed for the Y-axis
            Quaternion yRotation = Quaternion.Euler(0, TurnSpeed * Time.deltaTime * -1, 0);

            // Apply the rotation to the object
            transform.rotation *= yRotation;
        }
    }

    void Start()
    {
        // makes the tire have no speed limit
        rb.maxAngularVelocity = Mathf.Infinity;
    }

    void Update()
    {
     turn();
    }

}
