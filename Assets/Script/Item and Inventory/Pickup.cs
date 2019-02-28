using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    [Tooltip("Removes the sprite from the map when picked up if 'true'")]
    public bool removeOnPickup = false;
    public string button = "Submit";
    bool inside = false;


    private void Update()
    {
        if (inside && Input.GetButtonDown(button) && PlayerMovement.canMove)
        {
            Inventory.instance.AddItem(item);
            if (removeOnPickup)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = false;
        }
    }
}
