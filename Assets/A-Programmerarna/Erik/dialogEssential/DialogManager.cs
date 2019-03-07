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
    [Tooltip("delay efter varje enskilda ljud (vid själva pratandet i dialogen)")]
    public float soundTimeDelay;
    [TextArea(5, 20)]
    public string Text;
}
public class CompleteConvesation
{
    [HideInInspector] public AudioClip[] startDialogueSound;
    [HideInInspector] public GameObject holder;
    [HideInInspector] public string displayText;
    [HideInInspector] public List<Dialogs> dialogs = new List<Dialogs>();
    [HideInInspector] public float startConversationDelay;
    [HideInInspector] public GameObject[] Answers;
    [HideInInspector] public UnityEvent events;
    [HideInInspector] public bool hasBeenRead;
}

public class DialogManager : MonoBehaviour
{

    //gör scriptet till en singelton => finns inget behov för gamobjekt.find osv
    //vid behov andvänds DialogManager.Instance
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

    [HideInInspector] public float soundDelay;
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
        AudioClip[] allStartSound,
        float startSundPitch,
        List<Dialogs> newDialog,
        GameObject[] newAnswers,
        UnityEvent newEvents,
        string textOnChose,
        GameObject holder,
        bool hasBeenRead)
    {
        CompleteConvesation newConversation = new CompleteConvesation();

        newConversation.startDialogueSound = allStartSound;
        newConversation.dialogs = newDialog;
        newConversation.Answers = newAnswers;
        newConversation.events = newEvents;
        newConversation.displayText = textOnChose;
        newConversation.holder = holder;
        newConversation.hasBeenRead = hasBeenRead;
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

    public void playStartDialogueSound()
    {
        List<AudioClip> allSounds = new List<AudioClip>();

        for (int i = 0; i < quedDialogs.Count; i++)
        {
            for (int x = 0; x < quedDialogs[i].startDialogueSound.Length; x++)
            {
                allSounds.Add(quedDialogs[i].startDialogueSound[x]);
            }
        }

        float pitch = 0;
        if (allSounds.Count > 0)
        {
            int random = Random.Range(0, allSounds.Count);

            AudioManager.instance.playSFXRandomPitch(
                allSounds[random],
                pitch);

            soundDelay = allSounds[random].length;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (soundDelay >= 0)
        {
            soundDelay -= Time.deltaTime;
        }

        ///ifall spelaren befinner sig i en dialog
        if (activeDialog != null)
        {

            //hinder vilket gör att en kod endast körs en gång
            if (callFunctionOnce && !stopRewriteText)
            {
                //aktiverar alla ui-eliment
                dialogTextUI.enabled = true;
                dialogNameTagUI.enabled = true;
                dialogPortraitImageUI.enabled = true;

                if (soundDelay < 0)
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


            if (soundDelay < 0)
            {
                //lettar efter dialog input
                if (Input.GetButtonDown("Submit"))
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
                            if (!activeDialog.holder.GetComponent<ContaningDialog>().hasBeenRead)
                            {
                                activeDialog.events.Invoke();
                            }
                            activeDialog.holder.transform.parent.gameObject.GetComponent<conversationCollection>().isActive = false;
                            activeDialog.holder.GetComponent<ContaningDialog>().hasBeenRead = true;
                            ChoseDialogue.Instance.leaveMultyChoiceDialogue();

                            activeDialog = null;
                            dialogTextUI.enabled = false;
                            dialogNameTagUI.enabled = false;
                            dialogPortraitImageUI.enabled = false;
                            isInDialogue = false;
                            PlayerMovement.canMove = true;

                            quedDialogs.Clear();
                            ChoseDialogue.Instance.gameObject.GetComponent<Image>().enabled = false;
                        }
                    }
                }
            }
        }
        if (activeDialog == null)
        {

            if (!isInDialogue)
            {


                //nollställer systemet ifall det inte finns någon dialog i kön
                //obs körs varje frame, oödigt; förbätring?
                dialogTextUI.text = "";
                dialogNameTagUI.text = "";
                dialogTextUI.enabled = false;
                dialogNameTagUI.enabled = false;
                dialogPortraitImageUI.enabled = false;
                dialogAt = 0;

                //for (int i = 0; i < quedDialogs.Count; i++)
                //{
                //    ContaningDialog dialogIndex = quedDialogs[i].holder.GetComponent<ContaningDialog>();
                //    if (QuestManager.Instance.questsExistsInCompletedQuests(dialogIndex.removeDialogIfQuestsHasCompleted) && dialogIndex.removeDialogIfQuestsHasCompleted.Count > 0)
                //    {
                //        Debug.Log("should search");
                //        quedDialogs[i].holder.gameObject.transform.parent.GetComponent<conversationCollection>().isRemoved(quedDialogs[i].holder);
                //        Destroy(quedDialogs[i].holder);
                //        quedDialogs.Clear();
                //        //quedDialogs.Remove(quedDialogs[i]);
                //    }
                //    if (!QuestManager.Instance.questsExistsInCompletedQuests(dialogIndex.instantiateDialogIfQuestsExistsInCompleted))
                //    {
                //        quedDialogs.Remove(quedDialogs[i]);
                //    }
                //    if (!QuestManager.Instance.questsExistsInCurrentQuests(dialogIndex.instantiateDialogIfQuestsExistsInCurrent))
                //    {
                //        quedDialogs.Remove(quedDialogs[i]);
                //    }

                //}
                if (quedDialogs.Count == 1)
                {
                    //letar efter en ny dialog och ifall det finns en
                    //aktiveras den
                    if (quedDialogs[0].holder.GetComponent<ContaningDialog>().canPlaySound)
                    {
                        playStartDialogueSound();
                    }

                    activeDialog = quedDialogs[0];
                    ChoseDialogue.Instance.forceOne(activeDialog);
                    activeDialog.hasBeenRead = true;
                    callFunctionOnce = true;
                    isInDialogue = true;
                    ChoseDialogue.Instance.gameObject.GetComponent<Image>().enabled = true;
                    PlayerMovement.canMove = false;
                }
                else if (quedDialogs.Count >= 2)
                {
                    playStartDialogueSound();
                    ChoseDialogue.Instance.enterMultyChoiceDialogue(quedDialogs);
                    callFunctionOnce = true;
                    isInDialogue = true;
                    ChoseDialogue.Instance.gameObject.GetComponent<Image>().enabled = true;
                    PlayerMovement.canMove = false;
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