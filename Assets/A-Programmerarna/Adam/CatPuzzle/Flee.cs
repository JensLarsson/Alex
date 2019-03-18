using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : CatBase
{
    public Vector3 direction;
    float time;
    Vector3 oldCatPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        direction = catAI.moveDirection;
        animator.SetInteger("moveDirectionX", (int)direction.x);
        animator.SetInteger("moveDirectionY", (int)direction.y);
        time = 0;
        oldCatPosition = new Vector3();
        animator.SetBool("rayHit", false);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time += Time.deltaTime;

        if (time > 0.3f)
        {
            if (Vector3.Distance(oldCatPosition, cat.transform.position) < 0.03f)
            {
                animator.SetBool("hitWall", true);
            }
            oldCatPosition = cat.transform.position;
            time = 0;
        }
        if (animator.GetFloat("distanceToPlayer") < 0.6f)
        {
            direction = catAI.moveDirection;
            animator.SetInteger("moveDirectionX", (int)direction.x);
            animator.SetInteger("moveDirectionY", (int)direction.y);
        }
        direction = new Vector3(animator.GetInteger("moveDirectionX"), animator.GetInteger("moveDirectionY"));
        catAI.rb2D.velocity = new Vector2(catAI.speed * direction.x, catAI.speed * direction.y);
		if (direction.x < 0.0f) {
			catAI.GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			catAI.GetComponent<SpriteRenderer> ().flipX = false;
		}
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}