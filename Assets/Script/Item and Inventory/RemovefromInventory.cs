using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovefromInventory : MonoBehaviour
{
    public Item item;

    public void removeFromInventory()
    {
        Inventory.instance.removeItem(item);
    }
}
