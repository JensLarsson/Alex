using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTrigger : MonoBehaviour
{

    public string scene;
    public bool locked = false;
    public AudioClip lockedAudioClip, unlockAudioClip, openAudioClip;

    [Header("empty for no key press")]
    public string button = "Submit";

    bool inside = false;


    public void unlock()
    {
        locked = false;
        AudioManager.instance.playSFXClip(unlockAudioClip);
    }

    private void Update()
    {
        if (button != "")
        {
            if (inside && !locked && Input.GetButtonDown(button) && PlayerMovement.canMove)
            {
                AudioManager.instance.playSFXClip(openAudioClip);
                SceneController.instance.loadScene(scene);
            }
            else if (inside && locked && Input.GetButtonDown(button) && PlayerMovement.canMove)
            {
                //medela att objektet är låst
                AudioManager.instance.playSFXClip(lockedAudioClip);
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inside = true;
            if (button == "")
            {
                SceneController.instance.loadScene(scene);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inside = false;
        }
    }
}
