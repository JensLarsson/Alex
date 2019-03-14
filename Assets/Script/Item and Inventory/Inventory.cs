using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemContainer
{
    public Item item;
    int amount;
    int Amount
    {
        get
        {
            return amount;
        }
        set
        {

        }
    }
}


public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;

    public List<Item> items;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void AddItem(Item item)
    {
        if (!items.Contains(item) && item != null)
        {
            items.Add(item);
            //item.amount++;
        }
        else
        {
            // item.amount++;
        }
    }
    public void removeItem(Item item)
    {
        if (items.Contains(item) && item != null)
        {
            items.Remove(item);
            //item.amount--;
        }
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
