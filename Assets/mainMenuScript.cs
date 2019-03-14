using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour
{
  //  static public mainMenuScript instance = null;

    public List<GameObject> Buttons = new List<GameObject>();

    void Awake()
    {
       // menuManager.Instance.menuState = menuManager.MenuState.mainMenu;
        ////Sätter Singleton instance
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
    }
}
