using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryMenu : MonoBehaviour
{
    public Image image;
    public Text ItemDescriptionArea;

    [Header("Menu Text List")]
    public float xStartPos, yStartPos, yOffset;
    public GameObject textPrefab;
    List<GameObject> menuFields = new List<GameObject>();
    public AudioClip audioClip;

    int menuIndex = 0;
    int MenuIndex
    {
        get
        {
            return menuIndex;
        }
        set
        {
            if (value >= menuFields.Count)
            {
                menuIndex = value;
                menuIndex -= menuFields.Count;
            }
            else if (value < 0)
            {
                menuIndex = value;
                menuIndex += menuFields.Count;
            }
            else
            {
                menuIndex = value;
            }
        }
    }

    private void Start()
    {
        settupMenu();
    }

    void settupMenu()
    {
        gameObject.SetActive(true);
        foreach (Item item in Inventory.instance.items)
        {
            addText(item);
        }

        moveMenu(0);
    }

    void addText(Item item)
    {
        GameObject newText = Instantiate(textPrefab, textPrefab.transform.position, new Quaternion(), transform);
        newText.GetComponent<Text>().text = item.name;
        menuFields.Add(newText);
        newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(xStartPos, yStartPos + yOffset * menuFields.Count);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveMenu(-1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveMenu(1);
        }
    }

    void moveMenu(int i)
    {
        menuFields[MenuIndex].GetComponent<Text>().color = Color.black;
        MenuIndex += i;
        menuFields[MenuIndex].GetComponent<Text>().color = Color.red;
        ItemDescriptionArea.text = Inventory.instance.items[menuIndex].description;
        image.sprite = Inventory.instance.items[menuIndex].sprite;
        if (i != 0)
        {
            
        }
    }


}
