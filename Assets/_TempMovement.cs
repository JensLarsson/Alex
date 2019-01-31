using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TempMovement : MonoBehaviour
{

    public float speed = 1.0f;

    Rigidbody2D RIG;
    bool kollided = false;
    // Use this for initialization
    void Start()
    {
        RIG = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            RIG.MovePosition(transform.position + transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            RIG.MovePosition(transform.position - transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            RIG.MovePosition(transform.position - transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RIG.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        kollided = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        kollided = false;
    }


}
