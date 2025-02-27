using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    public Rigidbody rb;
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
        Vector3 moveDirection = new(direction.x, 0f, direction.y);
        moveDirection.y = 0f;

        float moveMultiplier;
        if (isGround)
        {
            moveMultiplier = 1f;
        }
        else
        {
            moveMultiplier = 0.3f; // 30% force in air
        }

         rb.AddForce(speed * moveDirection * moveMultiplier, ForceMode.Acceleration);
    }

    public void JumpPlayer()
    {
        if (jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x* 0.5f, 0, rb.linearVelocity.z* 0.5f); 
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
