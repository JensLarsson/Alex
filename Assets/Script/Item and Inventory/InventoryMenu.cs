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
        foreach (Item item in Inventory.instance.items)
        {
            addText(item);
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

        if (Input.GetButtonDown("Submit") && !buttonPressed)
        {
            StartCoroutine(shake(menuFields[MenuIndex]));
            foreach (GameObject gObject in CollisionTracking.collisionList)
            {
                InteractWithItem iWI = gObject.GetComponent<InteractWithItem>();

                if (iWI != null)
                {
                    if (!iWI.useItem(Inventory.instance.items[menuIndex])) AudioManager.instance.playSFXClip(unusableClip);
                }
            }
        }
    }


    //Bläddrar genom listan av text gameobjects (menuFields) för vissuella effekter.
    //Bläddrar även genom items listan i Inventory.instance för information för valt item.
    void moveMenu(int i)
    {
        menuFields[MenuIndex].GetComponent<Text>().color = Color.black;
        MenuIndex += i;
        menuFields[MenuIndex].GetComponent<Text>().color = Color.red;
        ItemDescriptionArea.text = Inventory.instance.items[menuIndex].description;
        image.sprite = Inventory.instance.items[menuIndex].sprite;
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
