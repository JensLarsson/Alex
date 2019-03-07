using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
class menuFunction
{
    [HideInInspector] public GameObject GO;
    public string ButtonTextUI;
    public UnityEvent newEvent;
    public UnityEvent newEventSec;
}

public class menuManager : MonoBehaviour
{

    [SerializeField] Material basic;
    [SerializeField] Material selected;
    GameObject Play;
    GameObject Developers;
    GameObject Exit;
    List<GameObject> Buttons = new List<GameObject>();

    [SerializeField] List<menuFunction> menuButtons = new List<menuFunction>();
    [SerializeField] string activeMenuButton;
    [HideInInspector] public  enum MenuState
    {
        mainMenu,
        menu,
        noMenu
    }
    [HideInInspector] public MenuState menuState;
    bool inisiate = false;

  [SerializeField] public static bool IsInMenu = false;


    [SerializeField] float xStartPos, yStartPos, ySpacing;
    [Tooltip("This text ui will determain the position and font/size of the text")]
	[SerializeField] GameObject textUIBase;
   List<GameObject> choseUI = new List<GameObject>();
    int menuIndex = 0;
    int MenuIndex
    {
        get
        {
            return menuIndex;
        }
        set
        {
            if (value >= menuButtons.Count)
            {
                menuIndex = value;
                menuIndex -= menuButtons.Count;
            }
            else if (value < 0)
            {
                menuIndex = value;
                menuIndex += menuButtons.Count;
            }
            else
            {
                menuIndex = value;
            }
        }
    }
    // Use this for initialization
    void Start ()
    {
        
	}
    void changeMenuState(MenuState newMenuState)
    {
        inisiate = false;
        menuState = newMenuState;        
    }
    public void returnToGame()
    {
        removeUI();
        changeMenuState(MenuState.noMenu);
        PlayerMovement.canMove = true;
    }
    public void nothing()
    {
        changeMenuState(MenuState.noMenu);
    }
    void addUI()
    {
        IsInMenu = true;
        for (int x = 0; x < menuButtons.Count; x++)
        {
            GameObject newText = Instantiate(textUIBase, textUIBase.transform.position, new Quaternion(), transform);
            newText.GetComponent<Text>().text = menuButtons[x].ButtonTextUI;
            newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(xStartPos, yStartPos + ySpacing * x);
            menuButtons[x].GO = newText.gameObject;
            choseUI.Add(newText);
        }
        gameObject.GetComponent<Image>().enabled = true;
        moveMenu(0);
    }
    void removeUI()
    {
        IsInMenu = false;
       foreach(menuFunction mf in menuButtons)
        {
            Destroy(mf.GO);
            choseUI.Remove(mf.GO);
        }
        gameObject.GetComponent<Image>().enabled = false;
    }
    void moveMenu(int i)
    {
        if (choseUI.Count > 0)
        {
            choseUI[MenuIndex].GetComponent<Text>().color = Color.black;
            MenuIndex += i;
            choseUI[MenuIndex].GetComponent<Text>().color = Color.red;
        }
    }
    void moveMainMenu()
    {
        if(menuIndex > Buttons.Count - 1)
        {
            menuIndex = Buttons.Count - 1;
        }
        if (menuIndex < 0)
        {
            menuIndex = 0;
        }
        for (int index = 0; index < Buttons.Count; index++)
        {
            if (index == menuIndex)
            {
                Buttons[index].gameObject.GetComponent<SpriteRenderer>().material = selected;
                //Buttons[index].gameObject.active = true;
            }
            else
            {
                Buttons[index].gameObject.GetComponent<SpriteRenderer>().material = basic;
               // Buttons[index].gameObject.active = false;
            }
        }
    }


    void selectAChoice()
    {
        removeUI();
        menuButtons[menuIndex].newEvent.Invoke();
    }
    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(menuState);
        switch (menuState)
        {
            #region not in menu
            case MenuState.noMenu:
                if(!inisiate)
                {
                    inisiate = true;
                }
                if (Input.GetButtonDown(activeMenuButton) && PlayerMovement.canMove == true)
                {
                    changeMenuState(MenuState.menu);
                    return;
                }
                break;
            #endregion

            #region in Menu
            case MenuState.menu:
                if (!inisiate)
                {
                    inisiate = true;
                    addUI();
                }
                PlayerMovement.canMove = false;
                if (Input.GetButtonDown(activeMenuButton))
                {
                    returnToGame();
                    return;
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    moveMenu(1);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    moveMenu(-1);
                }
                if (Input.GetButtonDown("Submit"))
                {
                    selectAChoice();
                }
                break;
            #endregion

            #region mainMenu
            case MenuState.mainMenu:
               
                if (!inisiate)
                {

                    for (int i = 0; i < transform.parent.transform.childCount; i++)
                    {
                        GameObject child = this.gameObject.transform.parent.transform.GetChild(i).gameObject;
                        if (!child == this.gameObject)
                        {
                            child.SetActive(false);
                        }
                    }
                    menuState = MenuState.mainMenu;

                    Play = GameObject.Find("play");
                    Developers = GameObject.Find("developers");
                    Exit = GameObject.Find("exit");

                    Buttons.Add(Play);
                    Buttons.Add(Developers);
                    Buttons.Add(Exit);
                    

                   
                    MenuIndex = 0;
                    moveMainMenu();

                    inisiate = true;
                }
                for (int i = 0; i < transform.parent.transform.childCount; i++)
                {
                    GameObject child = this.gameObject.transform.parent.transform.GetChild(i).gameObject;
                    if (!child == this.gameObject)
                    {
                        child.SetActive(false);
                    }
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    menuIndex--;
                    moveMainMenu();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    menuIndex++;
                    moveMainMenu();
                }
                if (Input.GetButtonDown("Submit"))
                {
                    Buttons[menuIndex].gameObject.GetComponent<mainMenuSelect>().onSelect.Invoke();
                }
                break;
                #endregion
        }
    }
}
