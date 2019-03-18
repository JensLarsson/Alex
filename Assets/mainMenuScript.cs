using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour
{
    public List<GameObject> Buttons = new List<GameObject>();


    void Start()
    {
        menuManager.Instance.menuState = menuManager.MenuState.mainMenu;
        menuManager.Instance.inisiate = false;
    }
}
//return, reset, music, sfx, main menu, exit game