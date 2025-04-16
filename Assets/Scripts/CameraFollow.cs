using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Referência ao personagem
    public Vector3 offset = new Vector3(0, 5, -10); // Posição relativa à do personagem
    public float smoothSpeed = 0.125f; // Velocidade do movimento suave

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
