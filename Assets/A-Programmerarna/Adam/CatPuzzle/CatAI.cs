using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CatAI : MonoBehaviour {

    public bool displayPath;
    public float speed;
    public float walkingRadius;
    [HideInInspector] public bool onPath = false;
    Vector3[] path;
    int targetIndex;
    public Rigidbody2D rb2D;

    Animator anim;
    public GameObject player;
    public GameObject cage;
    public LayerMask visionMask;

    public Vector3 moveDirection;
    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject GetCage()
    {
        return cage;
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        anim.SetInteger("moveDirectionY", 1);
        rb2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("distanceToPlayer", Vector3.Distance(player.transform.position, transform.position));
        Vector3 direction = player.transform.position - transform.position;
        direction = NormalizeDirections(direction.normalized);
        moveDirection = new Vector3(-direction.x, -direction.y);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(anim.GetInteger("moveDirectionX"), anim.GetInteger("moveDirectionY")), Mathf.Infinity, visionMask);
        Debug.DrawRay(transform.position, new Vector2(anim.GetInteger("moveDirectionX") * 20, anim.GetInteger("moveDirectionY") * 20), Color.red);
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

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            onPath = true;
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = transform.position;
        if (path.Length > 0)
        {
            currentWaypoint = path[0];
        }
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    onPath = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null && displayPath)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], new Vector3(0.2f, 0.2f));

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "cage")
        {
            //Win
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