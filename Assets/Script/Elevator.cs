using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Floor
{
    public string floorLabel, SceneName;
    public Image floorLight;
}
//Återanvänder menu skriptet för jag var för lat för att göra en klass som hanterar menuer

public class Elevator : MonoBehaviour
{

    public static Elevator Instance = null;

    public List<Floor> floors = new List<Floor>();

    public Sprite selectionLight, nonSelectedLight;
    public AudioClip moveButtonClip, unusableClip, travelSound;
    public float travelTime;

    bool buttonPressed = false;
    bool previousControll;

    int menuIndex = 1;
    int MenuIndex
    {
        get
        {
            return menuIndex;
        }
        set
        {
            if (value >= floors.Count)
            {
                menuIndex = value;
                menuIndex -= floors.Count;
            }
            else if (value < 0)
            {
                menuIndex = value;
                menuIndex += floors.Count;
            }
            else
            {
                menuIndex = value;
            }
        }
    }
    private void OnEnable()
    {
        PlayerMovement.canMove = false;
        moveMenu(0);
        menuManager.IsInMenu = true;
    }

    private void OnDisable()
    {
        PlayerMovement.canMove = true;
        menuManager.IsInMenu = false;
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
            if (floors[MenuIndex].SceneName != SceneManager.GetActiveScene().name)
            {
                SceneController.instance.loadScene(floors[MenuIndex].SceneName);
                this.gameObject.SetActive(false);
            }
        }
    }


    //Bläddrar genom listan av text gameobjects (menuFields) för vissuella effekter.
    //Bläddrar även genom items listan i Inventory.instance för information för valt item.
    void moveMenu(int i)
    {
        floors[menuIndex].floorLight.sprite = nonSelectedLight;
        MenuIndex += i;
        floors[menuIndex].floorLight.sprite = selectionLight;
        if (i != 0 && moveButtonClip != null)
        {
            AudioManager.instance.playSFXClip(moveButtonClip, true);
        }
    }
}
