using UnityEngine;
using Cinemachine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;
    private float rotationLimit = 70f;
    private float currentRotation = 0f;
    public float boostMultiplier = 2f;
    public float groundCheckDistance = 0.1f;

    private Rigidbody rb;
    private Animator animator;

    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise; // Reference to the Cinemachine noise

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Get the Cinemachine noise component
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.W);

        animator.SetBool("isBoosting", isBoosting);

        Vector3 movement = transform.forward;
        float effectiveMoveSpeed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;

        rb.AddForce(movement * effectiveMoveSpeed, ForceMode.Force);

        if (horizontalInput != 0 && IsGrounded())
        {
            float rotationAngle = horizontalInput > 0 ? -rotationSpeed * Time.deltaTime : rotationSpeed * Time.deltaTime;

            if (Mathf.Abs(currentRotation + rotationAngle) <= rotationLimit)
            {
                transform.Rotate(0f, rotationAngle, 0f, Space.World);
                currentRotation += rotationAngle;
            }
        }
    }

    bool IsGrounded()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        float radius = 0.5f;

        if (sphereCollider != null)
        {
            radius = sphereCollider.radius;
        }

        return Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance + radius);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ShakeCamera();
        }
    }

    void ShakeCamera()
    {
        if (virtualCamera != null && virtualCameraNoise != null)
        {
            StartCoroutine(Shake(30f, 30f));
        }
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        virtualCameraNoise.m_AmplitudeGain = magnitude;
        yield return new WaitForSeconds(duration);
        virtualCameraNoise.m_AmplitudeGain = 0f;
    }
}