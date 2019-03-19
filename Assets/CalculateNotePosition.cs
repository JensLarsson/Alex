using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CalculateNotePosition : MonoBehaviour {

    [SerializeField] public List<GameObject> pianoNotes = new List<GameObject>();
    [SerializeField] private float offset;
    [SerializeField] private float position;
    [SerializeField] private int wichNoteIsBoss;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {
        for (int i = 0; i < transform.childCount; i++)
        {
            
            if (!pianoNotes.Contains(transform.GetChild(i).gameObject))
            {
                pianoNotes.Add(transform.GetChild(i).gameObject);
            }

            if(pianoNotes[i] == null)
            {
                pianoNotes.RemoveAt(i);
            }

            pianoNotes[i].name = "Piano Tangent " + (i + 1);
        }

        OrganizeNotes();

	}

    void OrganizeNotes()
    {
        for(int i = 0; i < pianoNotes.Count; i++)
        {
            if( i == wichNoteIsBoss)
            {
                pianoNotes[i].transform.localPosition = new Vector2(position, 0f);
            }
            else if(i < wichNoteIsBoss)
            {
                float count = wichNoteIsBoss - i;
                float destination = position - offset * count;

                pianoNotes[i].transform.localPosition = new Vector2(destination, 0f);
            }
            else if(i > wichNoteIsBoss)
            {
                float count = Mathf.Abs(wichNoteIsBoss - i);
                float destination = position + offset * count;

                pianoNotes[i].transform.localPosition = new Vector2(destination, 0f);
            }
        }
    }

}
