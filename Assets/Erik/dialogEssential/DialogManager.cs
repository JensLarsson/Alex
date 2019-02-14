using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;
using UnityEngine.Events;

//This is what a dialog must contain, if player is not be able to respons
//leave playerResponses empty
//obs each object in playerResponses must contain a istrigger collider and ContainDialog-Script


[System.Serializable]

public class Dialogs
{
    public string NameOfTalkingNPC;
    public Sprite PortraitOfTalkingNPC;
    [Tooltip("antal sekunder det tar för en bokstav att animeras fram")]
    public float AnimationSpeed = 0.05f;
    public AudioClip[] soundThatPlayDuringDialogue;
    public float soundPitch;
    public float soundTimeDelay;
    [TextArea(5, 20)]
    public string Text;
}
public class CompleteConvesation
{

	[HideInInspector] public string displayText;
	[HideInInspector] public List<Dialogs> dialogs = new List<Dialogs>();
    [HideInInspector] public float startConversationDelay;
    [HideInInspector] public GameObject[] Answers;
    [HideInInspector] public UnityEvent events;
}

public class DialogManager : MonoBehaviour
{

    //gör scriptet till en singelton => finns inget behov för gamobjekt.find osv
    //vid behov andvänds DialogManager.Instance  (note, bör dock inte behövas utöver containingdialog scriptet!)
    private static DialogManager instance;
    public static DialogManager Instance { get { return instance; } }

    //en bool som kollar ifall spelaren är i en dialog (hindrar från att texten ska skrivas oändligt med gånger)
    bool callFunctionOnce = false;
    //hindrar texten från att bli null och skriver om texten igen efter den är klar
    bool stopRewriteText = false;
    //en bool som håller koll på ifall man kan hoppa över dialogen
    bool skipAnimation = false;

    [HideInInspector] public bool isInDialogue;


    //ui element
    [SerializeField] Text dialogTextUI;
    [SerializeField] Text dialogNameTagUI;
    [SerializeField] Image dialogPortraitImageUI;
    //en int som håller koll på vilken text ruta spelaren beffiner sig vid 
    //(likt en for loop, men variablen "i" är tillgänglig över hela scriptet)
    int dialogAt;

    CompleteConvesation newConversation = new CompleteConvesation();

    public CompleteConvesation activeDialog;
    public List<CompleteConvesation> quedDialogs = new List<CompleteConvesation>();
    //List<GameObject[]> alexAvailableAnswers = new List<GameObject[]>();

    //säkerställer så att det inte finns flera DialogManager
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There is too many dialogManager placed on scene");
        }
        activeDialog = null;


		isInDialogue = false;
		//nollställer systemet ifall det inte finns någon dialog i kön
		//obs körs varje frame, oödigt; förbätring?
		dialogTextUI.text = "";
		dialogNameTagUI.text = "";
		dialogTextUI.enabled = false;
		dialogNameTagUI.enabled = false;
		dialogPortraitImageUI.enabled = false;
		dialogAt = 0;
	}
    //en funktion som kallas vid nya dialoger
    public void queNewDialog(
		List<Dialogs> newDialog,
		GameObject[] newAnswers,
		float startConversationClippLength, 
		UnityEvent newEvents,
		string textOnChose)
    {
        newConversation.dialogs = newDialog;
        newConversation.Answers = newAnswers;
        newConversation.startConversationDelay = startConversationClippLength;
        newConversation.events = newEvents;
		newConversation.displayText = textOnChose;
        //Debug.Log(startConversationClippLength);
        quedDialogs.Add(newConversation);
    }

    public void createAnswers()
    {

        //ifall nya objekt ska skapas efter dialogen görs det här
        if (activeDialog.Answers.Length > 0)
        {
            if (dialogAt == activeDialog.dialogs.Count - 1)
            {
                List<GameObject> allAnswers = new List<GameObject>();
                GameObject newAnswer;
                for (int i = 0; i < activeDialog.Answers.Length; i++)
                {

                    newAnswer = Instantiate(activeDialog.Answers[i].gameObject);
                    allAnswers.Add(newAnswer);
                }
                foreach (GameObject thisAnswer in allAnswers)
                {
                    if (thisAnswer.GetComponent<ContaningDialog>().hasBeenRead)
                    {
                        Destroy(thisAnswer.gameObject);
                    }
                    else
                    {
                        thisAnswer.GetComponent<ContaningDialog>().siblings = allAnswers;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

		if (activeDialog == null)
		{
			if (quedDialogs.Count == 1)
			{
				isInDialogue = false;
				//nollställer systemet ifall det inte finns någon dialog i kön
				//obs körs varje frame, oödigt; förbätring?
				dialogTextUI.text = "";
				dialogNameTagUI.text = "";
				dialogTextUI.enabled = false;
				dialogNameTagUI.enabled = false;
				dialogPortraitImageUI.enabled = false;
				dialogAt = 0;
				//letar efter en ny dialog och ifall det finns en
				//aktiveras den
				if (quedDialogs.Count > 0)
				{
					activeDialog = quedDialogs[0];
					callFunctionOnce = true;
				}
			}
			else if (quedDialogs.Count >= 2)
			{
				ChoseDialogue.Instance.UpdateUI(true, quedDialogs);
			}
		}
		
        ///ifall spelaren befinner sig i en dialog
        if (activeDialog != null)
        {
            if (activeDialog.startConversationDelay >= 0)
            {
                activeDialog.startConversationDelay -= Time.deltaTime;
            }
            isInDialogue = true;
            //hinder vilket gör att en kod endast körs en gång
            if (callFunctionOnce && !stopRewriteText)
            {
				ChoseDialogue.Instance.UpdateUI(false, quedDialogs);
				//aktiverar alla ui-eliment
				dialogTextUI.enabled = true;
                dialogNameTagUI.enabled = true;
                dialogPortraitImageUI.enabled = true;

                if (activeDialog.startConversationDelay < 0)
                {

                    //startar animationen för texten (även ljuded?)
                    StartCoroutine(animateText(activeDialog.dialogs[dialogAt].Text));
                    //hindrar från återspelning av animation och ljud
                    callFunctionOnce = false;
                }
                //ändrar profilen till hon/han som pratar
                dialogPortraitImageUI.sprite = activeDialog.dialogs[dialogAt].PortraitOfTalkingNPC;
                dialogNameTagUI.text = activeDialog.dialogs[dialogAt].NameOfTalkingNPC;

                createAnswers();
            }


            if (activeDialog.startConversationDelay < 0)
            {
                //lettar efter dialog input
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //ifall man är mitten av en animation kan man hoppa över den
                    if (!callFunctionOnce)
                    {
                        skipAnimation = true;
                    }
                    else
                    {
                        //om man inte är i en...
                        dialogAt++;
                        stopRewriteText = false;
                        dialogTextUI.text = "";
                        dialogNameTagUI.text = "";

                        //nollställer dialogManager efter en dialog, samt tar bort dialogen ur listan
                        if (dialogAt >= activeDialog.dialogs.Count)
                        {
                            activeDialog.events.Invoke();
                            activeDialog = null;
                            dialogTextUI.enabled = false;
                            dialogNameTagUI.enabled = false;
                            dialogPortraitImageUI.enabled = false;
                            quedDialogs.Remove(quedDialogs[0]);
                        }
                    }
                }
            }
        }
    }

    IEnumerator playSound()
    {
        if (activeDialog.dialogs[dialogAt].soundThatPlayDuringDialogue.Length > 0)
        {
            while (true)
            {
                int random = Random.Range(0, activeDialog.dialogs[dialogAt].soundThatPlayDuringDialogue.Length);

                AudioManager.instance.playSFXRandomPitch(
                   activeDialog.dialogs[dialogAt].soundThatPlayDuringDialogue[random],
                   activeDialog.dialogs[dialogAt].soundPitch);
                yield return new WaitForSeconds(
                    activeDialog.dialogs[dialogAt].soundTimeDelay +
                    activeDialog.dialogs[dialogAt].soundThatPlayDuringDialogue[random].length);
            }
        }
    }

    //funktion för att ta fram bokstav för bokstav...
    IEnumerator animateText(string TextToDisplay)
    {
        string displayingString = "";
        int letterDisplayed = 0;
        IEnumerator PlaySound = playSound();
        StartCoroutine(PlaySound);
        while (letterDisplayed < TextToDisplay.Length)
        {
            if (skipAnimation)
            {
                break;
            }
            displayingString += TextToDisplay[letterDisplayed++];

            yield return new WaitForSeconds(activeDialog.dialogs[dialogAt].AnimationSpeed);
            dialogTextUI.text = displayingString;
        }
        StopCoroutine(PlaySound);

        dialogTextUI.text = TextToDisplay;
        callFunctionOnce = true;
        stopRewriteText = true;
        skipAnimation = false;
        yield return null;
    }
}