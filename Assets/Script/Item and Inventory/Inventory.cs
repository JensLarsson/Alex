using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance = null;

    public List<Item> items;

    void Awake () {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}


    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public bool hasItem(Item item)
    {
        foreach (Item i in items)
        {
            if (i == item)
            {
                return true;
            }
        }
        return false;
    }
}
