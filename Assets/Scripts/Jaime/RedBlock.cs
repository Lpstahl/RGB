using System;
using UnityEngine;

public class RedBlock : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public Action<RedBlock> blockDied;

    public void Revive(Vector3 inicialPosition, Quaternion inicialRotation)
    {
        this.transform.SetPositionAndRotation(inicialPosition, inicialRotation);
        gameObject.SetActive(true);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;     
    }
    public void DeactivateBlock()
    {
        blockDied?.Invoke(this);
        gameObject.SetActive(false);
    }
}
