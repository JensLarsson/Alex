using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryMenu : MonoBehaviour
{
    public Image image;
    public Text ItemDescriptionArea;

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
    int MenuIndex   //Loops through the available indexes 
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
        MenuIndex = 0;
        settupMenu();
        menuManager.Instance.menuState = menuManager.MenuState.inventory;
        PlayerMovement.canMove = false;
        menuManager.IsInMenu = true;
    }

    private void OnDisable()
    {
        PlayerMovement.canMove = true;
        menuManager.IsInMenu = false;
        menuManager.Instance.menuState = menuManager.MenuState.noMenu;
    }

    //Skapar en ny lista av interaktivbara textobjekt som kan användas som en meny
    void settupMenu()
    {
        clearList();
        foreach (itemContainer it in Inventory.instance.items)
        {
            addText(it);
        }
        moveMenu(0);
        buttonPressed = false;
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


    //Skapar en ny rad i listan av interaktivbara texter
    void addText(itemContainer item)
    {
        GameObject newText = Instantiate(textPrefab, textPrefab.transform.position, new Quaternion(), transform);
        newText.GetComponent<Text>().text = item.item.name;
        if (item.Amount > 1) newText.GetComponent<Text>().text += " x" + item.Amount;
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

        if (Input.GetButtonDown("Submit") && !buttonPressed)
        {
            StartCoroutine(shake(menuFields[MenuIndex]));

            for (int i = CollisionTracking.collisionList.Count - 1; i >= 0; i--)
            {
                InteractWithItem iWI = CollisionTracking.collisionList[i].GetComponent<InteractWithItem>();

                if (iWI != null)
                {
                    Item it = Inventory.instance.items[menuIndex].item;
                    int itemsUsed = iWI.useItem(Inventory.instance.items[menuIndex]);
                    Debug.Log(itemsUsed);
                    if (itemsUsed == 0)
                    {
                        AudioManager.instance.playSFXClip(unusableClip);
                    }
                    else
                    {
                        if (it.useSound != null) //Checks if there is a sound clip
                        {
                            AudioManager.instance.playSFXClip(Inventory.instance.items[menuIndex].item.useSound);
                        }
                        if (it.deleteOnUse) //Check if item should be deleted on use
                        {
                            Debug.Log("Removing");
                            Inventory.instance.removeItem(Inventory.instance.items[menuIndex].item, itemsUsed);
                            //if (Inventory.instance.items[menuIndex].Amount <= 0)
                            //{

                            //}
                        }
                        this.gameObject.SetActive(false);
                        PlayerMovement.canMove = true;
                        break;
                    }
                }
                else
                {
                    AudioManager.instance.playSFXClip(Inventory.instance.items[menuIndex].item.useSound);
                }
            }
        }
    }


    //Bläddrar genom listan av text gameobjects (menuFields) för vissuella effekter.
    //Bläddrar även genom items listan i Inventory.instance för information för valt item.
    void moveMenu(int i)
    {
        menuFields[MenuIndex].GetComponent<Text>().color = textColour;
        MenuIndex += i;
        menuFields[MenuIndex].GetComponent<Text>().color = selectionColour;
        ItemDescriptionArea.text = Inventory.instance.items[menuIndex].item.description;
        if (Inventory.instance.items[menuIndex].item.sprite != null)
        {
            image.sprite = Inventory.instance.items[menuIndex].item.sprite;
            image.color = Color.white;
        }
        else
        {
            image.sprite = null;
            image.color = new Color(0, 0, 0, 0);
        }
        if (i != 0 && moveButtonClip != null)
        {
            AudioManager.instance.playSFXClip(moveButtonClip, true);
        }
    }


    IEnumerator shake(GameObject gObject)
    {
        buttonPressed = true;
        Vector2 pos = gObject.transform.position;
        Timer timer = new Timer(buttonpressTime);
        while (!timer.expired)
        {
            timer.Time += Time.deltaTime;
            Vector3 vec = new Vector2(buttonpressForce, 0.0f);
            gObject.transform.position += vec;
            yield return new WaitForEndOfFrame();
        }
        gObject.transform.position = pos;
        buttonPressed = false;
    }
}
