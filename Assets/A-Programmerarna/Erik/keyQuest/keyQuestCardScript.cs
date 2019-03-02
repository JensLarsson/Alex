using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyQuestCardScript : MonoBehaviour {

    Animation anim;
    public void playCardAnimation(AnimationClip clippToPlay)
    {
        anim.clip = clippToPlay;
        anim.Play();
    }

    // Use this for initialization
    void Start ()
    {
        if(gameObject.GetComponent<Animation>() == null)
        {
            gameObject.AddComponent<Animation>();
        }
        anim = gameObject.GetComponent<Animation>();		
	}
}
