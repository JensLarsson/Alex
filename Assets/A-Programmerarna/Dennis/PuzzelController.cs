using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelController : MonoBehaviour
{
    [SerializeField]
    private GameObject Puzzel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!Puzzel.activeSelf)
                {
                    Puzzel.SetActive(true);
                    PlayerMovement.canMove = false;
                }
                else if (Puzzel.activeSelf)
                {
                    Puzzel.SetActive(false);
                    PlayerMovement.canMove = true;
                }
            }
        }
    }
}
