using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NoteBehaviour : MonoBehaviour
{
    [SerializeField] UnityEvent afterQuestIsDone;
    [SerializeField] Sprite activeB;
    [SerializeField] Sprite deactiveB;
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
        pianoNotes[currentNote].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
    }

    private void OnEnable()
    {

    }


    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            PlayerMovement.canMove = true;
            this.gameObject.SetActive(false);
        }


        ApplyInput();

        if (playerNoteOrder.Count == correctNoteOrder.Count)
        {
            if (CheckIfCorrectOrder())
            {
                Debug.Log("Correct Order!");
                puzzelController.updateChanges();
                afterQuestIsDone.Invoke();
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
                    if (!pianoNotes[currentNote].GetComponent<playNoteAudio>().hasBeenSelected)
                    {
                        pianoNotes[currentNote].GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        pianoNotes[currentNote].GetComponent<Image>().color = Color.gray;
                    }
                    if (pianoNotes[currentNote].GetComponent<playNoteAudio>().canBeSelected)
                    {
                        pianoNotes[currentNote].GetComponent<Outline>().enabled = false;
                    }
                    else
                    {
                        pianoNotes[currentNote].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = deactiveB;
                    }
                    currentNote -= 1;
                    if (pianoNotes[currentNote].GetComponent<playNoteAudio>().canBeSelected)
                    {
                        pianoNotes[currentNote].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                        pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        pianoNotes[currentNote].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = activeB;
                    }
              
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if (currentNote != (pianoNotes.Count - 1))
                {

                    if (!pianoNotes[currentNote].GetComponent<playNoteAudio>().hasBeenSelected)
                    {
                        pianoNotes[currentNote].GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        pianoNotes[currentNote].GetComponent<Image>().color = Color.gray;
                    }
                    if (pianoNotes[currentNote].GetComponent<playNoteAudio>().canBeSelected)
                    {
                        pianoNotes[currentNote].GetComponent<Outline>().enabled = false;
                    }
                    else
                    {
                        pianoNotes[currentNote].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = deactiveB;
                    }
                    currentNote += 1;
                    if (pianoNotes[currentNote].GetComponent<playNoteAudio>().canBeSelected)
                    {
                        pianoNotes[currentNote].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                        pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        pianoNotes[currentNote].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = activeB;
                    }
                }
            }
        }
        playNoteAudio pianoNote = gameObject.GetComponent<CalculateNotePosition>().pianoNotes[currentNote].gameObject.GetComponent<playNoteAudio>();

        if (Input.GetButtonDown("Submit") && !pianoNote.hasBeenSelected)
        {
            if (pianoNote.canBeSelected)
            {
                if (playerNoteOrder.Count < (correctNoteOrder.Count))
                {
                    playerNoteOrder.Add(currentNote);
                    pianoNote.PlayNoteAudio();
                    //pianoNote.GetComponent<Image>().material.color = selectedColor;
                    pianoNote.hasBeenSelected = true;
                    pianoNote.gameObject.GetComponent<Image>().color = Color.gray;
                    if (playerNoteOrder[playerNoteOrder.Count - 1] != correctNoteOrder[playerNoteOrder.Count - 1])
                    {
                        for (int i = 0; i < gameObject.GetComponent<CalculateNotePosition>().pianoNotes.Count; i++)
                        {
                            //pianoNote.hasBeenSelected = false;
                            gameObject.GetComponent<CalculateNotePosition>().pianoNotes[i].gameObject.GetComponent<playNoteAudio>().hasBeenSelected = false;
                            gameObject.GetComponent<CalculateNotePosition>().pianoNotes[i].gameObject.GetComponent<Image>().color = Color.white;
                        }
                        playerNoteOrder.Clear();
                        pianoNote.gameObject.GetComponent<Image>().color = new Color(0.7f,0.7f,0.7f);
                    }
                }
            }
            else
            {
                pianoNote.PlayNoteAudio();
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
