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
        for (int i = 0; i < DialogManager.Instance.gameObject.transform.childCount; i++)
        {
            DialogManager.Instance.gameObject.transform.parent.GetChild(i).gameObject.SetActive(true);
        }

        SceneController.instance.loadScene("Alex");
    }
}
