using UnityEngine;

public class rbMovement : MonoBehaviour
{
    [Header("Constants")]
    public Rigidbody rb;
    // Unity controls and constants input
    public float AccelerationMod;
    public float maxRotationSpeed = 500.0f; // Maximum rotation speed
    public float acceleration = 100.0f; // Acceleration rate
    public float DecelerationMod;
    public bool autoLockCursor;
    public float MaximumMovementSpeed = 100f;

    [Header("Controls")]
    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;

    private void Start()
    {
        Cursor.lockState = (autoLockCursor) ? CursorLockMode.Locked : CursorLockMode.None;
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is used for physics-related updates
    private void FixedUpdate()
    {
        if(IsGrounded())
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        // Key input detection
        if (Input.GetKey(Forwards))
        {
            moveDirection += transform.forward;
        }

        if (Input.GetKey(Backwards))
        {
            moveDirection -= transform.forward;
        }

        // Apply acceleration to the velocity
        rb.velocity += moveDirection.normalized * acceleration * Time.fixedDeltaTime;

        // Clamp the velocity magnitude
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaximumMovementSpeed);
    }

    private void HandleRotation()
    {   
        // Adjust rotation speed based on input
        if (Input.GetKey(Left))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -maxRotationSpeed);

        }
        
        if (Input.GetKey(Right))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * maxRotationSpeed);
        }
    }

    private bool IsGrounded() {
        //Simple way to check for ground
        if (Physics.Raycast (transform.position, Vector3.down, 1.5f))
        {
            return true;
        }
        else {
            return false;
        }
    }
}
