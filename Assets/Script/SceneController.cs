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
    public Dictionary<string, Vector3> lastScenePosition = new Dictionary<string, Vector3>();


    bool transitioning = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            image = GetComponent<Image>();
        }
        // else
        //{
        //   Destroy(this.gameObject);
        //}
    }

    public void loadScene(Scene scene)
    {
        StartCoroutine(SceneTransition(scene.name));
    }
    public void loadScene(string scene)
    {
        StartCoroutine(SceneTransition(scene));
    }
    public void loadScene(string scene, bool instant)
    {
        SceneManager.LoadScene(scene);
    }
    public void loadScene(string scene, float time)
    {
        StartCoroutine(SceneTransition(scene, time));
    }

    void saveScene(string scene)
    {
        //skicka string av scen till save klassen
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator SceneTransition(string scene, float f = 0.0f)
    {

        if (!transitioning && !DialogManager.Instance.isInDialogBranch)
        {
            saveScene(scene);
            transitioning = true;
            PlayerMovement.canMove = false;
            string id = SceneManager.GetActiveScene().name;
            if (PlayerTracker.Instance != null)
            {
                if (lastScenePosition.ContainsKey(id))
                {
                    lastScenePosition[id] = PlayerTracker.Instance.gameObject.transform.position;
                }
                else
                {
                    lastScenePosition.Add(id, PlayerTracker.Instance.gameObject.transform.position);
                }
            }

            Color colour;
            while (image.color.a < 1)
            {
                colour = new Color(0, 0, 0, image.color.a + Time.deltaTime * transitionIncrement);
                image.color = colour;
                yield return new WaitForEndOfFrame();
            }

            SceneManager.LoadScene(scene);
            yield return new WaitForSeconds(f);

            while (image.color.a > 0)
            {
                colour = new Color(0, 0, 0, image.color.a - Time.deltaTime * transitionIncrement);
                image.color = colour;
                yield return new WaitForEndOfFrame();
            }
            transitioning = false;
            if (!DialogManager.Instance.isInDialogue) PlayerMovement.canMove = true;
        }

    }
}