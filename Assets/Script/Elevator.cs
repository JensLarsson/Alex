using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    [Header("Menu Text List")]
    public GameObject textPrefab;
    public Color textColour = Color.white, selectionColour = Color.red;
    public float xStartPos, yStartPos, yOffset;
    List<GameObject> menuFields = new List<GameObject>();
    public AudioClip moveButtonClip, unusableClip;


    bool buttonPressed = false;
    bool previousControll;

    int menuIndex = 0;
    int MenuIndex
    {
        get
        {
            return menuIndex;
        }
        set
        {
            if (value >= menuFields.Count)
            {
                menuIndex = value;
                menuIndex -= menuFields.Count;
            }
            else if (value < 0)
            {
                menuIndex = value;
                menuIndex += menuFields.Count;
            }
            else
            {
                menuIndex = value;
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
