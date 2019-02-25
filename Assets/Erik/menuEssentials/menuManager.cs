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
}
public class menuManager : MonoBehaviour
{

    [SerializeField] List<menuFunction> menuButtons = new List<menuFunction>();
    [SerializeField] string activeMenuButton;
     public  enum MenuState
    {
        mainMenu,
        menu,
        noMenu
    }
    public MenuState menuState;
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
        
        menuState = MenuState.noMenu;
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
        //lol i lied
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
                    Debug.Log("once");
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    selectAChoice();
                }
                break;
            #endregion

            #region mainMenu
            case MenuState.mainMenu:
                if (!inisiate)
                {
                    inisiate = true;
                }
                break;
                #endregion
        }
	}
}
