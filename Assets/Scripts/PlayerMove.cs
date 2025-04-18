using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float SprintMultiplier = 2f;
    public float JumpForce = 5f;
    public float Gravity = -9.81f;
    public Transform PlayerCamera;
    public float LookSensitivityX = 2f;
    public float LookSensitivityY = 2f;
    public float MinYLookAngle = -90f;
    public float MaxYLookAngle = 90f;

    private CharacterController characterController;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleCameraRotation();
    }

    private void HandleMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Movement relative to where the player is facing
        Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        moveDirection.Normalize();

        float speed = WalkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= SprintMultiplier;
        }

        // Ground check and jumping
        if (characterController.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Small downward force to keep player grounded
            }

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(JumpForce * -2f * Gravity);
            }
        }
        else
        {
            velocity.y += Gravity * Time.deltaTime; // Apply gravity
        }

        // Move the character (combine horizontal movement and gravity)
        Vector3 finalMovement = moveDirection * speed + Vector3.up * velocity.y;
        characterController.Move(finalMovement * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * LookSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * LookSensitivityY;

        // Rotate the player (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera (vertical rotation)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, MinYLookAngle, MaxYLookAngle);
        PlayerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}

