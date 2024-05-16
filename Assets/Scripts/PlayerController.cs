using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))] // Ensure that the GameObject has an Animator component
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f; // Speed of rotation around the Y-axis
    private float rotationLimit = 70f; // Maximum rotation in degrees
    private float currentRotation = 0f; // Current rotation around the Y-axis
    public float boostMultiplier = 2f; // Speed multiplier for the boost
    public float groundCheckDistance = 0.1f; // Distance to check for ground

    private Rigidbody rb;
    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal input (A and D keys or left and right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Check if the player is pressing the shift key or the "W" key
        bool isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.W);

        // Set the isBoosting parameter in the Animator
        animator.SetBool("isBoosting", isBoosting);

        // Calculate the movement direction based on the camera's forward vector
        Vector3 movement = transform.forward;

        // Apply the boost multiplier if the shift key or the "W" key is pressed
        float effectiveMoveSpeed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;

        // Apply a constant force to the player in the direction of the camera's forward vector
        rb.AddForce(movement * effectiveMoveSpeed, ForceMode.Force);

        // Check if the player is pressing "A" or "D" and if they are grounded
        if (horizontalInput != 0 && IsGrounded())
        {
            // Calculate the new rotation angle
            float rotationAngle = horizontalInput > 0 ? -rotationSpeed * Time.deltaTime : rotationSpeed * Time.deltaTime;

            // Check if the new rotation would exceed the limit
            if (Mathf.Abs(currentRotation + rotationAngle) <= rotationLimit)
            {
                // Rotate the player around the Y-axis within the limit
                transform.Rotate(0f, rotationAngle, 0f, Space.World);
                currentRotation += rotationAngle; // Update the current rotation
            }
        }
    }

    // Check if the player is grounded
    bool IsGrounded()
{
    // Check for a SphereCollider component
    SphereCollider sphereCollider = GetComponent<SphereCollider>();
    float groundCheckDistance = 0.1f; // Default ground check distance
    float radius = 0.5f; // Default radius for ground check

    if (sphereCollider != null)
    {
        // Use the radius of the SphereCollider for the ground check
        radius = sphereCollider.radius;
    }

    // Perform a raycast downwards to check for ground
    return Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance + radius);
}
}