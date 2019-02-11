using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWithItem : MonoBehaviour {

    public UnityEvent _event;
    public Item item;

    public bool useItem(Item i)
    {
        if (item == i)
        {
            _event.Invoke();
            return true;
        }
        return false;
    }
}
