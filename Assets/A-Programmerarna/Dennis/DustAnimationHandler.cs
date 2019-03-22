using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAnimationHandler : MonoBehaviour {

    MoveOnCollision move;
    Animator anim;
	// Use this for initialization
	void Start () {
        move = GetComponentInParent<MoveOnCollision>();
        anim = GetComponent<Animator>();
        move.playDustAnim += PlayDustAnim;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void PlayDustAnim()
    {
        Debug.Log("Hello there");
        anim.SetBool("playDust", true);
    }
}
