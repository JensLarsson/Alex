using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeItem : MonoBehaviour
{
    public void RemoveItem(Item item)
    {
        Inventory.instance.removeItem(item);
    }
}
