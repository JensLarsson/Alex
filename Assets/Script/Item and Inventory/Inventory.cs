using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemContainer
{
    public Item item;
    int amount;
    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            if (value <= 0)
            {
                Inventory.instance.removeItem(item);
            }
            else
            {
                amount = value;
            }
        }
    }
}


public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;

    public List<itemContainer> items;
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
        bool exist = false;
        foreach (itemContainer it in items)
        {
            if (it.item == item)
            {
                it.Amount++;
                exist = true;
            }
        }
        if (!exist)
        {
            itemContainer _item = new itemContainer();
            _item.item = item;
            _item.Amount++;
        }
    }
    public bool removeItem(Item item)
    {
        foreach (itemContainer it in items)
        {
            if (it.item == item)
            {
                it.Amount--;
                return true;
            }
        }
        return false;
    }

    public bool hasItem(Item item)
    {
        foreach (itemContainer it in items)
        {
            if (it.item == item)
            {
                return true;
            }
        }
        return false;
    }
}
