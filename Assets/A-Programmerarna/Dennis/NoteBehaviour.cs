using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehaviour : MonoBehaviour
{

    List<GameObject> pianoNotes = new List<GameObject>();
    private List<int> playerNoteOrder = new List<int>();
    public List<int> correctNoteOrder = new List<int>();
    [SerializeField] PuzzelController puzzelController;
    int currentNote;

    public event System.Action playAudio;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!pianoNotes.Contains(transform.GetChild(i).gameObject))
            {
                pianoNotes.Add(transform.GetChild(i).gameObject);
            }

        }

        currentNote = pianoNotes.Count / 2;
        pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyInput();

        if (playerNoteOrder.Count == correctNoteOrder.Count)
        {
            if (CheckIfCorrectOrder())
            {
                Debug.Log("Correct Order!");
                puzzelController.updateChanges();
            }
            else if (!CheckIfCorrectOrder())
            {
                Debug.Log("GO AGAIN!!!!!!");
                playerNoteOrder.Clear();
            }

        }
    }

    void ApplyInput()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (currentNote != 0)
                {
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = false;
                    currentNote -= 1;
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if (currentNote != (pianoNotes.Count - 1))
                {
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = false;
                    currentNote += 1;
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerNoteOrder.Count < (correctNoteOrder.Count))
            {
                playerNoteOrder.Add(currentNote);
                gameObject.GetComponent<CalculateNotePosition>().pianoNotes[currentNote].gameObject.GetComponent<playNoteAudio>().PlayNoteAudio();
            }
        }
    }
    bool CheckIfCorrectOrder()
    {
        for (int i = 0; i < playerNoteOrder.Count; i++)
        {
            if (playerNoteOrder[i] != correctNoteOrder[i])
            {
                //playerNoteOrder.Clear();
                return false;
            }
        }
        return true;
    }

}
