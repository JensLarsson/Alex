using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
    Animator animator;
    Rigidbody2D rb2d;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        animator.SetFloat("VelocityY", rb2d.velocity.y);
        animator.SetFloat("VelocityX", rb2d.velocity.x);
    }
}
