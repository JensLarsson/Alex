using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractOnKeyPress : MonoBehaviour
{
    public string button = "Submit";
    public UnityEvent unityEvent;
    bool inside = false;


    private void Update()
    {
        if (inside && Input.GetButtonDown("Submit"))
        {
            unityEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            inside = false;
        }
    }
}
