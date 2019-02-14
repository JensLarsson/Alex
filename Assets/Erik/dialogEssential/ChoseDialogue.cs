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
    int max = 3;
    int current = 2;

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
    void playThisDialogue(CompleteConvesation dialogue, List<CompleteConvesation> allPossibleDialogue)
    {
        List<CompleteConvesation> queTheRest = new List<CompleteConvesation>();
        queTheRest = allPossibleDialogue;
        for(int x = 0; x < queTheRest.Count; x++)
        {
            if(queTheRest[x] == dialogue)
            {
                queTheRest.Remove(queTheRest[x]);
                break;
            }
        }
        DialogManager.Instance.quedDialogs.Clear();
        DialogManager.Instance.quedDialogs.Add(dialogue);
        StartCoroutine(putBackTheDialogue(queTheRest));
    }
    IEnumerator putBackTheDialogue(List<CompleteConvesation> remainingDialogue)
    {
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < remainingDialogue.Count; i++)
        {
            DialogManager.Instance.quedDialogs.Add(remainingDialogue[i]);
        }
    }
    #endregion

    public void UpdateUI(bool changeTo, List<CompleteConvesation> dialogue)
	{
		canChose = changeTo;
        if (canChose) {
            foreach (CompleteConvesation con in dialogue)
            {
                GameObject newText = Instantiate(textUIBase, textUIBase.transform.position, new Quaternion(), transform);
                newText.GetComponent<Text>().text = con.displayText;
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(xStartPos, yStartPos + ySpacing * choseUI.Count);
                choseUI.Add(newText);
            }


        }
		
	}

	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.E))
        {
            current++;
            if (current > max)
            {
                current = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            current--;
            if (current < 0)
            {
                current = max;
            }
        }
	}
}


#region probeblycrapp

//if (canChose)
//		{
//			for (int x = 0; x<dialogue.Count; x++)
//			{
//				newReply.text.text = dialogue[x].displayText;
//				newReply.textIndex = x;
//				replies.Add(newReply);
//			}

//			for (int x = 0; x<replies.Count; x++)
//			{
//				replies[x].text.enabled = canChose;

//				replies[x].textIndex = x;

//				replies[x].text.text = dialogue[x].displayText;
				

//				replies[x].text.transform.position =
//					new Vector3(
//                        textUIBase.transform.position.x,
//                        textUIBase.transform.position.y + (ofset* x),
//                        textUIBase.transform.position.z);
//			}
//		}

//		//enable the text
//		for (int x = 0; x<choseUI.Length; x++)
//		{
//			choseUI[x].active = canChose;
//		}

//        if (!canChose)
//		{
//			for (int i = 0; i<replies.Count; i++)
//			{
//				replies.Remove(replies[i]);
//			}
//		}

#endregion
