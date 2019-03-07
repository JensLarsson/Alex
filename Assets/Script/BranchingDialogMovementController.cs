using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BranchingDialogMovementController : MonoBehaviour
{

    public float standStillActionTime = 10.0f;
    public UnityEvent walkLeft, walkRight, walkUp, walkDown, standStill;
    Timer timer = new Timer();

    private void Start()
    {
        timer.Duration = standStillActionTime;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Up") && walkUp != null && PlayerMovement.canMove) { invokeAction(walkUp); }
        if (Input.GetButtonDown("down") && walkDown != null && PlayerMovement.canMove) { invokeAction(walkDown); }
        if (Input.GetButtonDown("Left") && walkLeft != null && PlayerMovement.canMove) { invokeAction(walkLeft); }
        if (Input.GetButtonDown("Right") && walkRight != null && PlayerMovement.canMove) { invokeAction(walkRight); }
        timer.Time += Time.deltaTime;
        if (timer.expired) { invokeAction(standStill); }
    }

    void invokeAction(UnityEvent even)
    {
        even.Invoke();
        Destroy(this.gameObject);
    }

}
