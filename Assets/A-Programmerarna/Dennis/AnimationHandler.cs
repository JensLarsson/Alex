﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    private int horizontal = 0;
    private int vertical = 0;

    private int horizontalAnim;
    private int verticalAnim;

    int previousX;
    int previousY;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            vertical = (int)verticalInput;

            if (Mathf.Abs(previousX) > 0)
            {
                verticalAnim = 0;
            }
            else
            {
                verticalAnim = (int)verticalInput;
            }
        }
        else
        {
            vertical = 0;
            verticalAnim = 0;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Mathf.Abs(previousY) > 0)
            {
                horizontalAnim = 0;
            }
            else
            {
                horizontalAnim = (int)horizontalInput;
            }
        }
        else
        {
            horizontal = 0;
            horizontalAnim = 0;
        }

        previousX = horizontalAnim;
        previousY = verticalAnim;

        animator.SetFloat("VelocityY", verticalAnim);
        animator.SetFloat("VelocityX", horizontalAnim);
    }
}