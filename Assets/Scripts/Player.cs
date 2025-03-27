using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;
    public float groundCheckRadius = 0.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        UpdateAnimations();
        Flip();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 movement = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0);
        rb.linearVelocity = movement;
    }

    void Flip()
    {
        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.forward = facingRight ? Vector3.right : Vector3.left;
        }
    }


    void Jump()
    {
        if (isGrounded)
        {
            // Ajusta a velocidade Y para aplicar a força do pulo
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

            // Marcar que o personagem não está mais no chão até ele cair de volta
            isGrounded = false;
        }
    }


        void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }
}