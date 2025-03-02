using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 10f; // Speed of rotation

    private Rigidbody rb;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private bool isGround = true;

    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnJump.AddListener(JumpPlayer);
        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector2 direction)
    {
        if (direction == Vector2.zero) return; // No movement input, so no need to rotate

        Vector3 moveDirection = CameraRelativeMovement(direction);

        float moveMultiplier = isGround ? 1f : 0.3f;

        rb.AddForce(speed * moveDirection * moveMultiplier, ForceMode.Acceleration);

        // 🔄 Rotate player to face movement direction
        RotatePlayer(moveDirection);
    }

    private Vector3 CameraRelativeMovement(Vector2 input)
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera Transform is not assigned!");
            return new Vector3(input.x, 0f, input.y);
        }

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return (forward * input.y + right * input.x).normalized;
    }

    private void RotatePlayer(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void JumpPlayer()
    {
        if (jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.5f, 0, rb.linearVelocity.z * 0.5f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            isGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            jumpCount = 0;
            isGround = true;
        }
    }
}
