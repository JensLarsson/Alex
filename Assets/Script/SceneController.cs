using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;
    public float transitionIncrement = 1.0f;
    Image image;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            image = GetComponent<Image>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void loadScene(Scene scene)
    {
        StartCoroutine(SceneTransition(scene.name));
    }
    public void loadScene(string scene)
    {
        StartCoroutine(SceneTransition(scene));
    }
    void saveScene(string scene)
    {
        //skicka string av scen till save klassen
    }

    IEnumerator SceneTransition(string scene)
    {
        Color colour;
        while (image.color.a < 1)
        {
            colour = new Color(0, 0, 0, image.color.a + Time.deltaTime * transitionIncrement);
            image.color = colour;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(scene);

        while (image.color.a > 0)
        {
            colour = new Color(0, 0, 0, image.color.a - Time.deltaTime * transitionIncrement);
            image.color = colour;
            yield return new WaitForEndOfFrame();
        }
    }


}
