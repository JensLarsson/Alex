using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    public void SetVelocityLeft()
    {
        AddVelocity(-speed * Time.deltaTime, 0f);
    }

    public void SetVelocityRight()
    {
        AddVelocity(speed * Time.deltaTime, 0f);
    }

    public void SetVelocityUp()
    {
        AddVelocity(0f, speed * Time.deltaTime);
    }

    public void SetVelocityDown()
    {
        AddVelocity(0f, -speed * Time.deltaTime);
    }
    void AddVelocity(float x, float y)
    {
        rb2d.velocity += new Vector2(x, y);
    }
}