using UnityEngine;

public class ZoneRedBlockDestroyer : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<RedBlock>() != null)
        {
            collision.gameObject.GetComponent<RedBlock>().DeactivateBlock();
        }
    }
}
