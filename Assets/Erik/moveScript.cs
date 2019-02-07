using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moveScript : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float speed = 1.5f;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position + transform.up * -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position + transform.right * -speed * Time.deltaTime);
        }

    }
}
