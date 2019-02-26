using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseDialogue : MonoBehaviour 
{
    int max;

    public float xStartPos, yStartPos, ySpacing;

	private static ChoseDialogue instance;
	public static ChoseDialogue Instance { get { return instance; } }

	

	List<GameObject> choseUI = new List<GameObject>();


    [Tooltip("This text ui will determain the position and font/size of the text")]
	[SerializeField] GameObject textUIBase;

    [HideInInspector]  bool playerHasToChose = false;

    List<CompleteConvesation> allReplies = new List<CompleteConvesation>();
   
    int menuIndex = 0;
    int MenuIndex
    {
        get
        {
            return menuIndex;
        }
        set
        {
            if (value >= choseUI.Count)
            {
                menuIndex = value;
                menuIndex -= choseUI.Count;
            }
            else if (value < 0)
            {
                menuIndex = value;
                menuIndex += choseUI.Count;
            }
            else
            {
                menuIndex = value;
            }
        }
    }

    // Use this for initialization
    void Start () {
        if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogError("There is too many ChoseDialogue placed on scene");
		}
    }

    public void enterMultyChoiceDialogue(List<CompleteConvesation> dialogueChoices)
	{
            
        playerHasToChose = true;

            for(int x = 0; x < dialogueChoices.Count; x++)
            {
                GameObject newText = Instantiate(textUIBase, textUIBase.transform.position, new Quaternion(), transform);
                newText.GetComponent<Text>().text = dialogueChoices[x].displayText;
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(xStartPos, yStartPos + ySpacing * choseUI.Count);
                choseUI.Add(newText);

            allReplies.Add(dialogueChoices[x]);
            }
      
        DialogManager.Instance.quedDialogs.Clear();
        moveMenu(0);
	}

    void selectAChoice()
    {
        DialogManager.Instance.quedDialogs.Clear();
        DialogManager.Instance.isInDialogue = false;
        allReplies[menuIndex].holder.GetComponent<ContaningDialog>().canPlaySound = false;
        DialogManager.Instance.quedDialogs.Add(allReplies[menuIndex]);
        cleanMultiDialogue();
        playerHasToChose = false;
    }

    [HideInInspector] public void leaveMultyChoiceDialogue()
    {
        for(int i = 0; i < allReplies.Count; i++)
        {
            if (allReplies[i] != allReplies[menuIndex])
            {
                allReplies[i].holder.GetComponent<ContaningDialog>().resetDialogue(true);
            }
            else
            {
                allReplies[i].holder.GetComponent<ContaningDialog>().resetDialogue(false);

            }
        }
        allReplies.Clear();
    }

    void cleanMultiDialogue()
    {
        for(int i = 0; i < choseUI.Count; i++)
        {
            Destroy(choseUI[i].gameObject);
        }
        choseUI.Clear();
    }

    void moveMenu(int i)
    {
        if (choseUI.Count > 0)
        {
            choseUI[MenuIndex].GetComponent<Text>().color = Color.black;
            MenuIndex += i;
            choseUI[MenuIndex].GetComponent<Text>().color = Color.red;
        }
    }

    // Update is called once per frame
    void Update ()
	{
        //Debug.Log("qued: " + DialogManager.Instance.quedDialogs.Count + ", replies: " + allReplies.Count);
        //Debug.Log(DialogManager.Instance.isInDialogue);
        if (playerHasToChose)
        {
            if (DialogManager.Instance.soundDelay <= 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    moveMenu(1);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    moveMenu(-1);
                }
                if (Input.GetButtonDown("Submit"))
                {
                    selectAChoice();
                }
            }
        }
    }

    public void exitDialogueWindow()
    {
        DialogManager.Instance.activeDialog = null;
        leaveMultyChoiceDialogue();
        DialogManager.Instance.quedDialogs.Clear();
        gameObject.GetComponent<Image>().enabled = false;
    }
}




