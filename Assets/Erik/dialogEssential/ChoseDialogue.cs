using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Replies
{
	public string text;
	public int textIndex;
}

public class ChoseDialogue : MonoBehaviour 
{
    int max;

    public float xStartPos, yStartPos, ySpacing;

	private static ChoseDialogue instance;
	public static ChoseDialogue Instance { get { return instance; } }

	[HideInInspector] public List<Replies> replies = new List<Replies>();
	Replies newReply = new Replies();

	List<GameObject> choseUI = new List<GameObject>();
	[SerializeField] float ofset = 0.5f;

    [Tooltip("This text ui will determain the position and font/size of the text")]
	[SerializeField] GameObject textUIBase;

    [HideInInspector]  bool canChose = false;

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

    void Awake()
    {
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
    #region confirmed Choice
    void playThisDialogue()
    {
        for (int x = 0; x < allReplies.Count; x++)
        {
            if (x == menuIndex)
            {
                DialogManager.Instance.quedDialogs.Add(allReplies[x]);
                break;
            }
        }      
       
        DialogManager.Instance.isInDialogue = false;

        
        StartCoroutine(putBackTheDialogue(allReplies));
    }
    IEnumerator putBackTheDialogue(List<CompleteConvesation> remainingDialogue)
    {
       yield return new WaitForSeconds(1);
        for (int i = 0; i < remainingDialogue.Count; i++)
        {
            DialogManager.Instance.quedDialogs.Add(remainingDialogue[i]);
        }
    }
    #endregion

    public void UpdateUI(bool changeTo, List<CompleteConvesation> dialogue)
	{
        canChose = changeTo;

        if (canChose)
        {
            choseUI.Clear();
            for(int x = 0; x < dialogue.Count; x++)
            {
                GameObject newText = Instantiate(textUIBase, textUIBase.transform.position, new Quaternion(), transform);
                newText.GetComponent<Text>().text = dialogue[x].displayText;
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(xStartPos, yStartPos + ySpacing * choseUI.Count);
                choseUI.Add(newText);
            }
            for (int y = 0; y < DialogManager.Instance.quedDialogs.Count; y++)
            {
                allReplies.Add(DialogManager.Instance.quedDialogs[y]);
            }
            
            DialogManager.Instance.quedDialogs.Clear();

            max = dialogue.Count;
            moveMenu(0);
        }
        else
        {
            allReplies.Clear();
            for (int i = 0; i < choseUI.Count; i++)
            {
                Destroy(choseUI[i].gameObject);
                if(choseUI[i] == null)
                {
                    choseUI.Remove(choseUI[i]);
                }
            }
        }
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
        if (canChose)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                moveMenu(1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                moveMenu(-1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playThisDialogue();
                UpdateUI(false, null);
            }
        }
    }
}


