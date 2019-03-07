using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBase : StateMachineBehaviour
{
    public GameObject cat;
    public GameObject opponent;
    public GameObject cage;
    public Vector3 target;
    public CatAI catAI;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat = animator.gameObject;
        catAI = cat.GetComponent<CatAI>();
        opponent = catAI.GetPlayer();
        cage = catAI.GetCage();
    }
}
