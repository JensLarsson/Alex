using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailManager : MonoBehaviour
{
    enum MoveState { move, wall, apple, bugApple };

    //LineRenderer lineRenderer;

    public GameObject blockPrefab;
    public float tickTimer = 1.0f;
    public static List<GameObject> positionOccupation = new List<GameObject>();

    public List<GameObject> tailPart = new List<GameObject>();
    //[SerializeField] Vector2 startDirection = Vector2.up; //For automaticMovement
    //Vector3 dir;                                          //For automaticMovement

    private void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        //dir = startDirection; //For automaticMovement
        //StartCoroutine(tick());//For automaticMovement
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            move(Vector2.up);
            //dir = Vector2.up;//For automaticMovement
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            move(Vector2.left);
            //dir = Vector2.left;//For automaticMovement
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            move(Vector2.down);
            //dir = Vector2.down;//For automaticMovement
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            move(Vector2.right);
            //dir = Vector2.right;//For automaticMovement
        }
    }

    //IEnumerator tick()//For automaticMovement
    //{
    //    while (tailPart.Count > 0)
    //    {
    //        yield return new WaitForSeconds(tickTimer);
    //        move(dir);
    //    }
    //}


    void move(Vector3 vec)
    {
        Vector3 pos = tailPart[0].transform.position + vec;

        MoveState moveState = MoveState.move;

        for (int i = positionOccupation.Count - 1; i >= 0; i--)
        {
            if (positionOccupation[i].transform.position == pos)
            {
                if (positionOccupation[i].tag == "Apple")
                {
                    moveState = MoveState.apple;
                    Destroy(positionOccupation[i]);
                }
                else if (positionOccupation[i].tag == "BuggApple")
                {
                    moveState = MoveState.bugApple;
                    Destroy(positionOccupation[i]);
                }
                else
                {
                    moveState = MoveState.wall;

                    if (positionOccupation[i] == tailPart[tailPart.Count - 1] && tailPart.Count > 2)
                    {
                        moveState = MoveState.move;
                    }

                    //for (int y = tailPart.Count - 1; y >= 0; y--)
                    //{
                    //    Destroy(tailPart[y]);
                    //}
                    //tailPart = new List<GameObject>();
                    //Debug.Log("Death");
                    //break;
                }
            }
        }

        switch (moveState)
        {
            case MoveState.move:
                if (tailPart.Count > 0)
                {
                    moveForwardLastBlock(pos);
                }
                break;


            case MoveState.apple:
                GameObject Tail = Instantiate(blockPrefab, pos, Quaternion.identity);
                tailPart.Insert(0, Tail);
                break;


            case MoveState.bugApple:
                int temp = tailPart.Count / 2;
                for (int y = tailPart.Count - 1; y > temp; y--)
                {
                    tailPart.RemoveAt(y);
                }
                moveForwardLastBlock(pos);
                break;
        }
        //List<Vector3> tempList= new List<Vector3>();
        //foreach(GameObject gObject in tailPart)
        //{
        //    tempList.Add(gObject.transform.position);
        //}
        //lineRenderer.positionCount = tempList.Count;
        //lineRenderer.SetPositions(tempList.ToArray());
        
    }

    void moveForwardLastBlock(Vector3 pos)
    {
        tailPart[tailPart.Count - 1].transform.position = pos;
        tailPart.Insert(0, tailPart[tailPart.Count - 1]);
        tailPart.RemoveAt(tailPart.Count - 1);
    }

}