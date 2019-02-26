using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PositionSubscriber))]
public class MoveOnCollision : MonoBehaviour
{
    private Vector2 startPos;
    public float timeToDestination = 1.0f;
    private float timeToLerp;
    private Vector2 differenceInPos;
    private Vector2 travelPos;
    private bool isLerping = false, buttonDown = false;
    public float narmeVarde;


    private void Start()
    {
        differenceInPos = transform.position;
        travelPos = transform.position;
        startPos = transform.position;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            buttonDown = true;
        }
        if (Input.GetButtonUp("Submit"))
        {
            buttonDown = false;
        }


        //startPos = transform.position;
        timeToLerp += Time.deltaTime;
        transform.position = Vector2.Lerp(startPos, travelPos, timeToLerp);
        if (Vector2.Distance(transform.position, travelPos) < narmeVarde && isLerping)
        {
            isLerping = false;
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            travelPos = transform.position;
            startPos = transform.position;
            timeToLerp = 0;
        }
    }

    void move(Collision2D collision)
    {
        if (!isLerping && buttonDown)
        {
            Vector3 intendedPosition = transform.position;
            startPos = transform.position;
            timeToLerp = 0;
            if (collision.transform.tag == "Player")
            {
                differenceInPos = -collision.transform.position + transform.position;
                if (Mathf.Abs(differenceInPos.x) > Mathf.Abs(differenceInPos.y))
                {
                    differenceInPos.y = 0;
                    intendedPosition = new Vector2(transform.position.x + differenceInPos.x / Mathf.Abs(differenceInPos.x), transform.position.y);

                }
                else
                {
                    differenceInPos.x = 0;
                    intendedPosition = new Vector2(transform.position.x, transform.position.y + differenceInPos.y / Mathf.Abs(differenceInPos.y));
                }
                Debug.Log(travelPos);
            }
            if (!PositionManager.Instance.isPositionOccupied(intendedPosition))
            {
                Debug.Log("Movint towards " + travelPos);
                travelPos = intendedPosition;
                isLerping = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        move(collision);
    }
}
