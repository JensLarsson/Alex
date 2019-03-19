using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CatAI : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb2D;
    SpriteRenderer sR;

    Animator anim;
    public GameObject player;
    public GameObject cage;
    public GameObject lightVision;
    public LayerMask visionMask;

    public Vector3 moveDirection;
    public GameObject GetPlayer()
    {
        return player;
    }

    public Sprite moveSprite;
    Sprite stopSprite;
    public UnityEvent victoryEvent;


    public GameObject GetCage()
    {
        return cage;
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("moveDirectionY", 1);
        rb2D = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        stopSprite = sR.sprite;
    }

    private void LateUpdate()
    {
        if (rb2D.velocity != Vector2.zero)
        {
            sR.sprite = moveSprite;
        }
        else
        {
            sR.sprite = stopSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetFloat("distanceToPlayer", Vector3.Distance(player.transform.position, transform.position));
        Vector3 direction = player.transform.position - transform.position;
        direction = NormalizeDirections(direction.normalized);
        moveDirection = new Vector3(-direction.x, -direction.y);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(anim.GetInteger("moveDirectionX"), anim.GetInteger("moveDirectionY")), Mathf.Infinity, visionMask);
        Debug.DrawRay(transform.position, new Vector2(anim.GetInteger("moveDirectionX") * 20, anim.GetInteger("moveDirectionY") * 20), Color.red);


        if (anim.GetInteger("moveDirectionX") != 0)
        {
            lightVision.transform.position = new Vector3(transform.position.x + 10 * anim.GetInteger("moveDirectionX"), transform.position.y);
            lightVision.transform.localScale = new Vector3(20, 0.5f, 1);
        }
        else if (anim.GetInteger("moveDirectionY") != 0)
        {
            lightVision.transform.position = new Vector3(transform.position.x, transform.position.y + 10 * anim.GetInteger("moveDirectionY"));
            lightVision.transform.localScale = new Vector3(0.5f, 20, 1);
        }
        if (hit)
        {
            Debug.Log("Hit Something");
            anim.SetInteger("moveDirectionX", (int)moveDirection.x);
            anim.SetInteger("moveDirectionY", (int)moveDirection.y);
            anim.SetBool("rayHit", true);
        }

    }

    Vector3 NormalizeDirections(Vector3 _direction)
    {
        Vector3 temp = new Vector3();
        if (_direction.x < 0.5f && _direction.x > -0.5f)
        {
            temp.x = 0;
        }
        else if (_direction.x >= 0.5f)
        {
            temp.x = 1;
        }
        else if (_direction.x <= -0.5f)
        {
            temp.x = -1;
        }

        if (temp.x == 0)
        {
            if (_direction.y < 0.5f && _direction.y > -0.5f)
            {
                temp.y = 0;
            }
            else if (_direction.y >= 0.5f)
            {
                temp.y = 1;
            }
            else if (_direction.y <= -0.5f)
            {
                temp.y = -1;
            }
        }
        return temp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "cage")
        {


            //Win
            victoryEvent.Invoke();

            gameObject.SetActive(false);
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            anim.SetBool("hitWall", true);

        }
        /*Vector3 direction = player.transform.position - transform.position;
        direction = direction.normalized;
        //Vector3.Angle(-direction, -col.contacts[0].normal);
        Debug.Log(Vector3.Angle(-direction, -col.contacts[0].normal));*/
    }
    /*void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            anim.SetBool("hitWall", true);
        }
    }*/
}