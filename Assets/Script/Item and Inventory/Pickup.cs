using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    public Item item;
    [Tooltip("Removes the sprite from the map when picked up if 'true'")]
    public bool removeOnPickup = false, accessOnlyThroughScript = false;
    public string button = "Submit";
    bool inside = false;
    public UnityEvent _event;


    private void Update()
    {
        if (inside && Input.GetButtonDown(button) && PlayerMovement.canMove && !accessOnlyThroughScript)
        {
            pickup();
        }
    }
    public void pickup()
    {
        Inventory.instance.AddItem(item);
        if (_event != null) _event.Invoke();
        if (removeOnPickup)
        {
            Destroy(this.gameObject);
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
