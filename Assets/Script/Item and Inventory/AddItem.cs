using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour {

    public void getItem(Item pick)
    {
        Inventory.instance.AddItem(pick);
    }
    public void removeItem(Item pick)
    {
        Inventory.instance.removeItem(pick);
    }

}
