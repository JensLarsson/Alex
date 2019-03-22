using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelController : MonoBehaviour
{
    [SerializeField]
    private GameObject Puzzel;
    [SerializeField] bool isInCollider;
    bool gotAllNotes = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInCollider)
        {
            if (Input.GetButton("Submit") && !Puzzel.activeSelf && menuManager.Instance.menuState == menuManager.MenuState.noMenu)
            {
                updateChanges();
            }
        }
    }
    public void setGotAllNotesToTrue()
    {
        gotAllNotes = true;
    }
    public void updateChanges()
    {
        if (gotAllNotes)
        {
            Puzzel.SetActive(true);
            PlayerMovement.canMove = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            isInCollider = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInCollider = false;

        }
    }
}