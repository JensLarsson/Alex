using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour {
    [SerializeField] float movingSpeed;
    [SerializeField] float textOfset;
    [Tooltip("this is a delay that manage how long the player needs to be in credits until can quick escape ack to main menu")]
    [SerializeField] float exitDelay;
    [SerializeField] GameObject exitText;
    [SerializeField] float exitAlphaChangeSpeed = 4.5f;
    Color exitColor;
    public bool canExit = false;
    [TextArea(50, 100)]
    [SerializeField] string credits;
    int lines = 1;
    float textHeight;
    float startY, currentY = 0, toY;

    
    

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<TextMesh>().text = credits;

        char[] charInCredits = credits.ToCharArray();
        for (int i = 0; i < charInCredits.Length; i++)
        {
            if(charInCredits[i].ToString() == "\n")
            {
                lines++;
            }
        }
        startY = 0 - (lines/ 2) + textOfset;
        toY = (Mathf.Sqrt(startY * startY) * 2) + textOfset;

        transform.position = new Vector3(
           0, startY + textOfset, 0);

        exitColor = exitText.GetComponent<TextMesh>().color;
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (canExit)
        {
            case true:
                //flera magiska konstanter, men de får inte ändras; ändras /2 kommer alphas landas att vara störe än intervalet 0-1
                //vilket leder till att färgen kommer antingen att fastna i 1 eller 0 längre än den ska
                //+0.5 fixar så att det inte kan bli negativa värden 
                exitColor.a = (Mathf.Sin(Time.time * exitAlphaChangeSpeed) / 2) + 0.5f;
                exitText.GetComponent<TextMesh>().color = exitColor;

                break;

            case false:
                {
                    if (exitDelay >= 0)
                    {
                        exitDelay -= Time.deltaTime;
                    }
                    else
                    {
                        exitText.SetActive(true);
                        canExit = true;
                    }
                }
                break;
        }

        if (currentY + textOfset >= toY)
        {
            credtisIsDone();
        }
        else
        {
            currentY += movingSpeed * Time.deltaTime;
            transform.position += Vector3.up * movingSpeed * Time.deltaTime;
        }
	}
    void credtisIsDone()
    {
        SceneController.instance.loadScene("main");
    }
}
