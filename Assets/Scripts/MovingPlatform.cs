using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform pointA;  // Ponto de início
    [SerializeField] private Transform pointB;  // Ponto final
    [SerializeField] private float speed = 2f; // Velocidade de movimento
    [SerializeField] private float waitTime = 1f; // Tempo de espera nos pontos

    [Header("Movement Type")]
    [Tooltip("Se marcado, a plataforma vai e volta continuamente")]
    [SerializeField] private bool pingPongMovement = true;

    [Tooltip("Se marcado, a plataforma começa se movendo para o ponto B")]
    [SerializeField] private bool startMovingToB = true;

    private Vector3 nextPosition;
    private float waitCounter;
    private bool isWaiting;

    void Start()
    {
        // Configura posições iniciais
        if (pointA == null) pointA = new GameObject("PointA").transform;
        if (pointB == null) pointB = new GameObject("PointB").transform;

        pointA.SetParent(null);
        pointB.SetParent(null);

        // Define a posição inicial
        nextPosition = startMovingToB ? pointB.position : pointA.position;
        transform.position = startMovingToB ? pointA.position : pointB.position;
    }

    void Update()
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
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

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
}