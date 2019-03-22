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
            amount = value;
            Debug.Log(amount + " " + item.name + " in Inventory");
        }
    }
}


public class Inventory : MonoBehaviour
{
    public AudioClip clip;
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
                AudioManager.instance.playSFXClip(clip);
                it.Amount++;
                exist = true;
            }
        }
        if (!exist)
        {
            AudioManager.instance.playSFXClip(clip);
            itemContainer _item = new itemContainer();
            _item.item = item;
            _item.Amount++;
            items.Add(_item);
        }
    }
    public bool removeItem(Item item, int i = 1)
    {
        foreach (itemContainer it in items)
        {
            if (it.item == item)
            {
                it.Amount -= i;

                if (it.Amount <= 0)
                {
                    items.Remove(it);
                }
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
