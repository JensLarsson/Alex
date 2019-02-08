using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTrigger : MonoBehaviour
{

    public string scene;

    [Header("empty for no key press")]
    public string button = "Submit";

    bool inside = false;


    private void Update()
    {
        if (inside && Input.GetButtonDown(button))
        {
            SceneController.instance.loadScene(scene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = true;
            if (button == "")
            {
                SceneController.instance.loadScene(scene);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = false;
        }
    }
}
