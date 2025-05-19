using UnityEngine;
using UnityEngine.Audio;

public class HoldPlatform : MonoBehaviour
{
    [SerializeField] bool move = true;
    [SerializeField] MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
           /* if (this.gameObject.GetComponent<BoxCollider>().enabled)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = true;
            }*/
            move = move ? false : true;
            if (mesh.enabled)
            {
                mesh.enabled = false;
            }
            else
            {
                mesh.enabled = true;
            }
        }
    }
    /*private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>() != null)
        {
            collision.gameObject.GetComponent<MovingPlatform>().ChangeSpeed(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>() != null)
        {
            collision.gameObject.GetComponent<MovingPlatform>().ChangeSpeed(true);
        }
    }*/

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>() != null)
        {
            collision.gameObject.GetComponent<MovingPlatform>().ChangeSpeed(move);
        }
    }
}
