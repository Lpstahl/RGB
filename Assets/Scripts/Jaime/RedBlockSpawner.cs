using UnityEngine;
using System.Collections.Generic;

public class RedBlockSpawner : MonoBehaviour
{
    [SerializeField] RedBlock prefabVermelho;
    [SerializeField] float timeBetweenSpawms = 1f;
    RedBlock currentBlock;
    Stack<RedBlock> blockStack = new Stack<RedBlock>();

    private void Start()
    {
        InvokeRepeating("SpawnBlock", 1, timeBetweenSpawms);
    }
    void Update()
    {
        //fazer timer para os blocos spawnerem
        
        /*if (Input.GetMouseButtonDown(0))
        {
            if (blockStack.Count > 0)
            {
                currentBlock = blockStack.Pop();
                currentBlock.Revive(this.transform.position, this.transform.rotation);
                currentBlock.blockDied += OnBlockDeath;
            }
            else
            {
                currentBlock = Instantiate(prefabVermelho, this.transform.position, this.transform.rotation);
                currentBlock.blockDied += OnBlockDeath;
            }
        }*/
    }

    void SpawnBlock()
    {
        if (blockStack.Count > 0)
        {
            currentBlock = blockStack.Pop();
            currentBlock.Revive(this.transform.position, this.transform.rotation);
            currentBlock.blockDied += OnBlockDeath;
        }
        else
        {
            currentBlock = Instantiate(prefabVermelho, this.transform.position, this.transform.rotation);
            currentBlock.blockDied += OnBlockDeath;
        }
    }

    void OnBlockDeath(RedBlock blockThatDied)
    {
        blockStack.Push(blockThatDied);
        blockThatDied.blockDied -= OnBlockDeath;
    }
}
