using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;  // Jump force applied
    public float moveSpeed = 5f;  // Walking speed
    public LayerMask groundLayer; // Layer mask to identify the ground

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Freeze rotation on X and Z axis to prevent the player from falling over
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        isGrounded = IsGrounded();

        HandleMovement();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position + Vector3.down * 0.1f, 0.2f, groundLayer);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        Vector3 currentVelocity = rb.velocity;
        currentVelocity.x = move.x * moveSpeed;
        currentVelocity.z = move.z * moveSpeed;

        rb.velocity = currentVelocity;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}