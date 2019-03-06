using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mainMenuSelect : MonoBehaviour {

    public UnityEvent onSelect;

    public void exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
    public void play()
    {
        SceneController.instance.loadScene("Alex");
    }
}
