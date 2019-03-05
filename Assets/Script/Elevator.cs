using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Floor
{
    public string floorLabel, SceneName;
}
//Återanvänder menu skriptet för jag var för lat för att göra en klass som hanterar menuer

public class Elevator : MonoBehaviour {

    public static Elevator Instance = null;

    public List<Floor> floors = new List<Floor>();

    [Header("Menu Text List")]
    public GameObject textPrefab;
    public Color textColour = Color.white, selectionColour = Color.red;
    public float xStartPos, yStartPos, yOffset;
    List<GameObject> menuFields = new List<GameObject>();
    public AudioClip moveButtonClip, unusableClip;
    public float buttonpressForce = 0.8f, buttonpressTime = 0.06f;


    bool buttonPressed = false;
    bool previousControll;

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

    private void OnEnable()
    {
        settupMenu();
        PlayerMovement.canMove = false;
        menuManager.IsInMenu = true;
    }

    private void OnDisable()
    {
        PlayerMovement.canMove = true;
        menuManager.IsInMenu = false;
    }



    //Skapar en ny lista av items som fins i Inventory klassen
    void settupMenu()
    {
        clearList();
        foreach (Floor floor in floors)
        {
            addText(floor);
        }
        moveMenu(0);
    }
    //Tar bort alla gameobjects som representerar item slots från menyn
    void clearList()
    {
        for (int i = menuFields.Count - 1; i >= 0; i--)
        {

            Destroy(menuFields[i]);
        }
        menuFields = new List<GameObject>();
    }


    //Lägger till items i listan av objekt
    void addText(Floor floor)
    {
        GameObject newText = Instantiate(textPrefab, textPrefab.transform.position, new Quaternion(), transform);
        newText.GetComponent<Text>().text = floor.floorLabel;
        menuFields.Add(newText);
        newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(xStartPos, yStartPos + yOffset * menuFields.Count);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveMenu(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveMenu(-1);
        }

        if (Input.GetButtonDown("Submit") && !buttonPressed)
        {
            SceneController.instance.loadScene(floors[MenuIndex].SceneName);
            this.gameObject.SetActive(false);
        }
    }


    //Bläddrar genom listan av text gameobjects (menuFields) för vissuella effekter.
    //Bläddrar även genom items listan i Inventory.instance för information för valt item.
    void moveMenu(int i)
    {
        menuFields[MenuIndex].GetComponent<Text>().color = textColour;
        MenuIndex += i;
        menuFields[MenuIndex].GetComponent<Text>().color = selectionColour;
        if (i != 0 && moveButtonClip != null)
        {
            AudioManager.instance.playSFXClip(moveButtonClip, true);
        }
    }
}
