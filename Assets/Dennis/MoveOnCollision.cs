using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnCollision : MonoBehaviour
{

    public GameObject movingBlock;

    private void OnTriggerEnter2D(Collider2D other)
    {

        Vector2 movingBlockPos = transform.position;
        Vector2 playerPos = other.transform.position;

        if (playerPos.y > movingBlockPos.y + movingBlock.transform.localScale.y / 2)
        {
            transform.position = Vector2.MoveTowards(movingBlockPos, new Vector2(movingBlockPos.x, movingBlockPos.y - 1), 1.0f);
        }
        else if (playerPos.y < movingBlockPos.y - movingBlock.transform.localScale.y / 2)
        {
            transform.position = Vector2.MoveTowards(movingBlockPos, new Vector2(movingBlockPos.x, movingBlockPos.y + 1), 1.0f);

        }
        else if (playerPos.x > movingBlockPos.x)
        {
            transform.position = Vector2.MoveTowards(movingBlockPos, new Vector2(movingBlockPos.x - 1, movingBlockPos.y), 1.0f);

        }
        else if (playerPos.x < movingBlockPos.x)
        {
            transform.position = Vector2.MoveTowards(movingBlockPos, new Vector2(movingBlockPos.x + 1, movingBlockPos.y + 1), 1.0f);

        }

    }
}
