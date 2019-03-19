using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : CatBase
{

    float radius = 5;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator,stateInfo,layerIndex);
        animator.SetBool("hitWall", false);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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

    /*Vector3 NewTarget()
    {
        Vector3 temp = new Vector3(Random.Range(cat.transform.position.x - catAI.walkingRadius, cat.transform.position.x + catAI.walkingRadius), Random.Range(cat.transform.position.y - catAI.walkingRadius, cat.transform.position.y + catAI.walkingRadius));
        while (Vector3.Distance(temp, cage.transform.position) < 2.5f)
        {
            temp = new Vector3(Random.Range(cat.transform.position.x - catAI.walkingRadius, cat.transform.position.x + catAI.walkingRadius), Random.Range(cat.transform.position.y - catAI.walkingRadius, cat.transform.position.y + catAI.walkingRadius));
        }
            //Debug.Log(Vector3.Distance(temp, cage.transform.position));
        return temp;
    }*/
}
