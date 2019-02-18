using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public float tickTimer = 1.0f;
    public static List<GameObject> positionOccupation = new List<GameObject>();

    public List<GameObject> tailPart = new List<GameObject>();
    public Vector2 startDirection = Vector2.up;
    Vector3 dir;

    private void Start()
    {
        dir = startDirection;
        StartCoroutine(tick());
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dir = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dir = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = Vector2.right;
        }
    }

    IEnumerator tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickTimer);
            Vector3 pos = tailPart[0].transform.position + dir;

            bool ate = false;

            for (int i = positionOccupation.Count - 1; i >= 0; i--)
            {
                if (positionOccupation[i].transform.position == pos)
                {
                    switch (positionOccupation[i].tag)
                    {
                        case "Apple":
                            ate = true;
                            Destroy(positionOccupation[i]);
                            break;
                    }
                }
            }
            if (ate)
            {
                GameObject Tail = Instantiate(blockPrefab, pos, Quaternion.identity);
                tailPart.Insert(0, Tail);
            }
            else
            {

                tailPart[tailPart.Count - 1].transform.position = pos;
                tailPart.Insert(0, tailPart[tailPart.Count - 1]);
                tailPart.RemoveAt(tailPart.Count - 1);
            }
        }
    }
}
