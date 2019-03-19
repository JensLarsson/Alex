using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playNoteAudio : MonoBehaviour {

    NoteBehaviour notes;
	// Use this for initialization
	void Start () {
        notes = GetComponentInParent<NoteBehaviour>();

        notes.playAudio += PlayNoteAudio;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void PlayNoteAudio()
    {

    }
}
