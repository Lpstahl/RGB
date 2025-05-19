using UnityEngine;
using UnityEngine.InputSystem;

public class Player3d : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    [SerializeField] Transform cam;
    [SerializeField] Animator anim;
    Vector2 input;
    Vector3 velAtual;
    Vector3 MoveDir;
    [SerializeField] float speed = 5f;
    //[SerializeField] float speedRotation = 5f;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
        UpdateDir();
    }

    private void FixedUpdate()
    {
        velAtual = MoveDir * speed;

        rb.linearVelocity = velAtual;
    }


    void UpdateDir()
    {
        MoveDir = cam.forward * input.y;
        MoveDir += cam.right * input.x;
        MoveDir.Normalize();

        tr.forward = MoveDir;
    }
    private void OnMove(InputValue inputMove)
    {
        Debug.Log($"Movendo para: {inputMove.Get<Vector2>()}");
        input = inputMove.Get<Vector2>();
    }

    void UpdateAnimations()
    {
        anim.SetFloat("InputY", input.y);
    }
}
