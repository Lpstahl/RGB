using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform pointA;  // Ponto de início
    [SerializeField] private Transform pointB;  // Ponto final
    [SerializeField] private float speed = 2f; // Velocidade de movimento
    [SerializeField] private float waitTime = 1f; // Tempo de espera nos pontos
    float originalSpeed;

    [Header("Movement Type")]
    [Tooltip("Se marcado, a plataforma vai e volta continuamente")]
    [SerializeField] private bool pingPongMovement = true;

    [Tooltip("Se marcado, a plataforma começa se movendo para o ponto B")]
    [SerializeField] private bool startMovingToB = true;

    private Vector3 nextPosition;
    private float waitCounter;
    private bool isWaiting;
    Rigidbody rb;

    void Start()
    {
        // Configura posições iniciais
        if (pointA == null) pointA = new GameObject("PointA").transform;
        if (pointB == null) pointB = new GameObject("PointB").transform;

        pointA.SetParent(null);
        pointB.SetParent(null);

        // Define a posição inicial
        nextPosition = startMovingToB ? pointB.position : pointA.position;
        //transform.position = startMovingToB ? pointA.position : pointB.position;

        rb = GetComponent<Rigidbody>();
        originalSpeed = speed;
    }

    void Update()
    {
        // Verifica se chegou ao destino
        if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
        {
            if (pingPongMovement)
            {
                nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
            }
            else
            {
                // Movimento unidirecional (volta instantaneamente)
                nextPosition = (nextPosition == pointB.position) ? pointA.position : pointB.position;
                transform.position = nextPosition;
                return;
            }

            isWaiting = true;
            waitCounter = waitTime;
        }
    }

    void FixedUpdate()
    {
        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0f)
            {
                isWaiting = false;
            }
            return;
        }
        // Move a plataforma
        rb.MovePosition(Vector3.MoveTowards(rb.position, nextPosition, speed * Time.deltaTime));
    }

    void OnDrawGizmos()
    {
        // Desenha os pontos e o caminho no editor
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Faz o jogador/personagem mover-se com a plataforma
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Libera o jogador/personagem da plataforma
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    public void ChangeSpeed (bool stop)
    {
        speed = (stop == true) ? 0f : originalSpeed;
    }
}