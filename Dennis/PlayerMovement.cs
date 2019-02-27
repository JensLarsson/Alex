using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;

    [HideInInspector]
    public static bool canMove;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        canMove = true;
        
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    public void SetVelocityLeft()
    {
        if (canMove)
        {
            AddVelocity(-speed * Time.deltaTime, 0f);
        }
        
    }

    public void SetVelocityRight()
    {
        if (canMove)
        {
            AddVelocity(speed * Time.deltaTime, 0f);
        }
        
    }

    public void SetVelocityUp()
    {
        if (canMove)
        {
            AddVelocity(0f, speed * Time.deltaTime);
        }
        
    }

    public void SetVelocityDown()
    {
        if (canMove)
        {
            AddVelocity(0f, -speed * Time.deltaTime);
        }
        
    }

    void AddVelocity(float x, float y)
    {
        rb2d.velocity += new Vector2(x, y);
    }
}