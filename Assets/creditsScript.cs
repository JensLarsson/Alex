using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour {
    [SerializeField] float movingSpeed;
    [TextArea(50, 100)]
    [SerializeField] string credits;
    int lines = 1;
    float textHeight;
    float startY, toY; 

    
    

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
        Debug.Log("amont of lines: " + lines);
        startY = transform.position.y;
        Debug.Log(startY);
        textHeight = ( 0.1f * lines * transform.localScale.y);
        toY = startY + textHeight;
        Debug.Log("distance : " + toY);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if(transform.position.y >= toY)
        {
            Debug.Log("credits is done");
            
        }
        else
        {
            transform.position += Vector3.up * movingSpeed * Time.deltaTime;
        }
		
	}
}
