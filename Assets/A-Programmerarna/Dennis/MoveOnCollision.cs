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
    public AudioClip[] pushClip = new AudioClip[0];
    public event System.Action playDustAnim;

    public GameObject axel, animation;
    private void Start()
    {
        differenceInPos = transform.position;
        travelPos = transform.position;
        startPos = transform.position;
    }
    private void Update()
    {
        if (Input.GetButton("Submit"))
        {
            buttonDown = false;
        }
        if (Input.GetButtonDown("Submit"))
        {
            buttonDown = true;
        }


        //startPos = transform.position;
        timeToLerp += Time.deltaTime * timeToDestination;
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

    void move(Collider2D collision)
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
                    axel.transform.localScale = new Vector3(differenceInPos.x / Mathf.Abs(differenceInPos.x), 1, 1);
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
                if (differenceInPos.x != 0) animation.SetActive(true);
                Debug.Log("Movint towards " + travelPos);
                AudioManager.instance.playSFXClip(pushClip[Random.Range(0, pushClip.Length)]);
                travelPos = intendedPosition;
                isLerping = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        move(collision);
    }
}
