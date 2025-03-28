using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed of the player
    public float rotationSpeed = 700f;  // Rotation speed for smooth rotation of the player

    private Transform playerCameraTransform;

    void Start()
    {
        // Find the Camera in the player prefab hierarchy (assuming it's a child object)
        playerCameraTransform = transform.GetComponentInChildren<Camera>().transform;
    }

    void FixedUpdate()
    {
        // Get input for movement (W, A, S, D or arrow keys)
        float horizontal = Input.GetAxis("Horizontal");  // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical");  // W/S or Up/Down Arrow

        // Get the direction the camera is facing (ignoring y-axis to avoid moving up/down)
        Vector3 cameraForward = playerCameraTransform.forward;
        cameraForward.y = 0;  // We don't want to move the player up or down, only on the X-Z plane
        cameraForward.Normalize();

        // Get the right direction of the camera
        Vector3 cameraRight = playerCameraTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Calculate the movement direction based on the input
        Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;

        // If there is input (movement is being requested), move the player
        if (moveDirection.sqrMagnitude > 0.01f)  // To prevent very small input values causing jitter
        {
            // Move the player in the direction calculated
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Optional: Rotate player to face the movement direction with smoothing
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        
    }
}
