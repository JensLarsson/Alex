using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
// main menu, reset room

[System.Serializable]
class menuFunction
{
    [HideInInspector] public GameObject GO;
    public string ButtonTextUI;
    public UnityEvent newEvent;
    public UnityEvent newEventSec;
}
[System.Serializable]
class soundFunc
{
    [HideInInspector] public GameObject GO;
    public string ButtonTextUI;
    public float value;
    public UnityEvent primeEvent;
    public UnityEvent secEvent;
}

public class menuManager : MonoBehaviour
{
    private static menuManager instance;
    public static menuManager Instance { get { return instance; } }

    [SerializeField] Material basic;
    [SerializeField] Material selected;
    [SerializeField] List<GameObject> Buttons = new List<GameObject>();

    [SerializeField] List<menuFunction> menuButtons = new List<menuFunction>();
    [SerializeField] List<soundFunc> soundButtons = new List<soundFunc>();
    [SerializeField] [Range(0, 1)] float sfxChange = 0.05f;
    [SerializeField] [Range(0, 1)] float musicChange = 0.05f;
    public int soundIndex = 0;

    [SerializeField] string exitMenuKey;
    [SerializeField] GameObject[] objectsToRemoveWhenInMenu;
    public enum MenuState
    {
        mainMenu,
        menu,
        noMenu,
        soundMenu
    }
    public MenuState menuState;
    

    [HideInInspector] public bool inisiate = false;

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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There is too many ChoseDialogue placed on scene");
        }
    }
    void changeMenuState(MenuState newMenuState)
    {
        inisiate = false;
        menuState = newMenuState;        
    }
    #region menu options
    public void returnToGame()
    {
        removeUI();
        changeMenuState(MenuState.noMenu);
        PlayerMovement.canMove = true;
    }
    public void audioSelect()
    {
        removeUI();
        addSoundUI();

    }

    public void exit()
    {
        Application.Quit();
    }
    public void IncSound(string soundType)
    {
        if(soundType == "sfx")
        {
            AudioManager.instance.sfxVolume += sfxChange;
           
        }
        else if(soundType == "music")
        {
            AudioManager.instance.musicVolume += musicChange;
            AudioManager.instance.setMusicVolume();
        }
        else
        {
            Debug.LogError(soundType + " not found, only sfx and music are allowed");
        }
        soundDisplayUpdate();
    }
    public void decSound(string soundType)
    {
        if (soundType == "sfx")
        {
            AudioManager.instance.sfxVolume -= sfxChange;
        }
        else if (soundType == "music")
        {
            AudioManager.instance.musicVolume -= musicChange;
            AudioManager.instance.setMusicVolume();
            
        }
        else
        {
            Debug.LogError(soundType + " not found, only sfx and music are allowed");
        }
        soundDisplayUpdate();
    }

    void soundDisplayUpdate()
    {
        soundButtons[0].value = AudioManager.instance.sfxVolume;
        soundButtons[1].value = AudioManager.instance.musicVolume;
        removesoundUI();
        addSoundUI();
    }
    #endregion

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
    void addSoundUI()
    {
        IsInMenu = true;
        menuState = MenuState.soundMenu;

        soundButtons[0].value = AudioManager.instance.sfxVolume;
        soundButtons[1].value = AudioManager.instance.musicVolume;

        choseUI.Clear();
        for (int x = 0; x < soundButtons.Count; x++)
        {
            int extraSpacing = 5;
            GameObject newText = Instantiate(textUIBase, textUIBase.transform.position, new Quaternion(), transform);
            newText.GetComponent<Text>().text = soundButtons[x].ButtonTextUI + System.Environment.NewLine + soundButtons[x].value;
            newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(xStartPos, yStartPos + ((extraSpacing + ySpacing) * x));
            menuButtons[x].GO = newText.gameObject;
            choseUI.Add(newText);
        }
        gameObject.GetComponent<Image>().enabled = true;
        moveSoundMenu(0);
        
    }
    void removesoundUI()
    {
        foreach(GameObject sf in choseUI)
        {
            Destroy(sf);
        }
        choseUI.Clear();
        gameObject.GetComponent<Image>().enabled = false;
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
    void moveSoundMenu(int a)
    {
        soundIndex += a;
        if(soundIndex >= soundButtons.Count)
        {
            soundIndex = 0;
        }
        if (soundIndex < 0)
        {
            soundIndex = soundButtons.Count - 1;
        }
        foreach(GameObject go in choseUI)
        {
            if(go == choseUI[soundIndex])
            {
                go.GetComponent<Text>().color = Color.red;
            }
            else
            {
                go.GetComponent<Text>().color = Color.black;
            }
        }
    }
    void moveMainMenu()
    {
        Debug.LogWarning("hmmm");
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
            }
            else
            {
                Buttons[index].gameObject.GetComponent<SpriteRenderer>().material = basic;
            }
        }
    }
    public void goToMenu()
    {
        SceneController.instance.loadScene("main", true);
        inisiate = false;
        removeUI();
        menuState = MenuState.mainMenu;
        
    }
    public void reloadScene()
    {
        menuState = MenuState.noMenu;
        SceneController.instance.resetScene();
        PlayerMovement.canMove = true;
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
                    foreach (GameObject go in objectsToRemoveWhenInMenu)
                    {
                        go.gameObject.SetActive(true);
                        Debug.Log("should exist now");
                    }

                    inisiate = true;
                }
                if (Input.GetButtonDown(exitMenuKey) && PlayerMovement.canMove == true)
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
                if (Input.GetButtonDown(exitMenuKey))
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

            #region sound
            case MenuState.soundMenu:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    soundButtons[soundIndex].primeEvent.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    soundButtons[soundIndex].secEvent.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    moveSoundMenu(1);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    moveSoundMenu(-1);
                }
                if (Input.GetButtonDown(exitMenuKey))
                {

                    removesoundUI();
                    menuState = MenuState.menu;
                    inisiate = false;
                    return;
                }

                break;
            #endregion

            #region mainMenu
            case MenuState.mainMenu:
                if (!inisiate)
                {
                    foreach(GameObject go in objectsToRemoveWhenInMenu)
                    {
                        go.gameObject.SetActive(false);
                    }

                    Buttons.Clear();
                    
                    MenuIndex = 0;
                    Buttons.Clear();
                    Buttons = GameObject.Find("MenuH").gameObject.GetComponent<mainMenuScript>().Buttons;
                    moveMainMenu();
                    inisiate = true;
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
