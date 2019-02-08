using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void loadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.name);
        saveScene(scene.name);
    }
    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        saveScene(scene);
    }

    void saveScene(string scene)
    {
        //skicka string av scen till save klassen
    }
}
