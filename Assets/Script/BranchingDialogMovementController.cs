﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchingDialogMovementController : MonoBehaviour
{
    [Tooltip("0 for deactivating Stand Still Action")]
    public float standStillActionTime = 0.0f;
    public conversationCollection walkLeft, walkRight, walkUp, walkDown, anyDirectio, standStill;
    Timer timer = new Timer();

    private void Start()
    {
        timer.Duration = standStillActionTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && walkUp != null && PlayerMovement.canMove) { invokeAction(walkUp); }
        if (Input.GetKeyDown(KeyCode.S) && walkDown != null && PlayerMovement.canMove) { invokeAction(walkDown); }
        if (Input.GetKeyDown(KeyCode.A) && walkLeft != null && PlayerMovement.canMove) { invokeAction(walkLeft); }
        if (Input.GetKeyDown(KeyCode.D) && walkRight != null && PlayerMovement.canMove) { invokeAction(walkRight); }
        if ((
            Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            && PlayerMovement.canMove)
        {
            timer.Time = 0.0f;
            if (anyDirectio != null) invokeAction(anyDirectio);
        }

        timer.Time += Time.deltaTime;
        if (timer.Duration > 0 && timer.expired) { invokeAction(standStill); }
    }

    void invokeAction(conversationCollection even)
    {
        even.gameObject.SetActive(true);
        even.onFunctionCall();
        this.gameObject.SetActive(false);
    }
}