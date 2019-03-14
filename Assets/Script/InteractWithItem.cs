using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWithItem : MonoBehaviour
{

    public UnityEvent _event;
    public Item item;
    public int numberOfItemsNeeded;

    public int useItem(itemContainer items)
    {
        if (items.item == item && items.Amount == numberOfItemsNeeded)
        {
            _event.Invoke();
            return numberOfItemsNeeded;
        }
        return 0;
    }
}
