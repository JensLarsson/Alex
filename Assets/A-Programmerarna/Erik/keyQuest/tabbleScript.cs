using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Cards
{
    [HideInInspector] public Vector3 pos;
    [HideInInspector] public GameObject card;
}

public class tabbleScript : MonoBehaviour
{
    [SerializeField] Material pointerMaterial;
    [SerializeField] Material defaultMaterial;
    [SerializeField] GameObject pointer;
    GameObject pointerControl;
    int pointerAtIndex = 1;
    [SerializeField] Transform cardGoTo;

    [SerializeField] [Range(0, 1)] float travelTimeStamp;


    [SerializeField] GameObject tabble;
    [SerializeField] GameObject[] cards;
    [SerializeField] AnimationClip pickUpAnimation;
    [SerializeField] AnimationClip putDownAnimation;

    public List<GameObject> GOInTabbleOverlay = new List<GameObject>();
    public List<Cards> existingCards = new List<Cards>();

    [Tooltip("ofset borde vara kortens bredd samt lite extra avstånd")]
    [SerializeField] float ofset = 1.2f;
    [Tooltip("startOfset borde vara extra avståndet som korten har kvar till kanten / 2 (på högra sidan)")]
    [SerializeField] float startOfset = 2;

    GameObject objectThePlayerLooksAt;
    bool lookAtCards = false;
    bool aCardIsZoomedIn = false;
    [SerializeField] Camera camera;

    bool inisiate = true;

    enum TabbleState
    {
        NotAtTabble,
        atTabbleNotLooking,
        atTabbleAndLooking
    }
    TabbleState tabbleState = TabbleState.NotAtTabble;

    // Use this for initialization
    void Start()
    {
        Debug.LogWarning("is possible to improve the card position when odd number (which it is)");
    }

    // Update is called once per frame
    void Update()
    {
        switch (tabbleState)
        {
            case TabbleState.atTabbleNotLooking:
                if (inisiate)
                {
                    PlayerMovement.canMove = true;
                    inisiate = false;
                }
                if (Input.GetButtonDown("Submit"))
                {
                    tabbleState = TabbleState.atTabbleAndLooking;
                    inisiate = true;
                }
                break;

            case TabbleState.atTabbleAndLooking:
                if (inisiate)
                {
                    #region create tabble
                    GameObject newTabble = Instantiate(tabble);

                    newTabble.transform.position =
                        new Vector3(
                            newTabble.transform.position.x,
                            newTabble.transform.position.y,
                            0);

                    GOInTabbleOverlay.Add(newTabble);
                    #endregion
                    #region create cards

                    for (int i = 0; i < cards.Length; i++)
                    {
                        GameObject newCard = Instantiate(
                       cards[i],
                       camera.ScreenToWorldPoint(
                           new Vector3(
                               Screen.width * 0.5f,
                               Screen.height * 0.5f,
                               0)),
                       Quaternion.identity);
                        //position fix

                        if (cards.Length % 2 == 0)
                        {
                               newCard.transform.position =
                               new Vector3(
                                   newCard.transform.position.x + startOfset + (-(ofset * cards.Length * 0.5f) + ofset * i),
                                   newCard.transform.position.y,
                                   0);
                        }
                        else
                        {
                         newCard.transform.position =
                              new Vector3(
                                  newCard.transform.position.x + startOfset + (-((ofset * cards.Length * 0.5f) - ofset * 0.5f) + ofset * i),
                                  newCard.transform.position.y,
                                  0);
                        }

                        for (int child = 0; child < newCard.transform.childCount; child++)
                        {
                            if (newCard.transform.GetChild(child).GetComponent<SpriteRenderer>() != null)
                            {
                                newCard.transform.GetChild(child).GetComponent<SpriteRenderer>().sortingOrder = 7;
                            }
                        }

                        GOInTabbleOverlay.Add(newCard);
                        
                        Cards newCards;
                        newCards.card = newCard;
                        newCards.pos = newCard.transform.position;
                        existingCards.Add(newCards);
                    }
                    #endregion
                    GameObject Pointer = Instantiate(pointer);
                    Pointer.GetComponent<SpriteRenderer>().sortingOrder = 8;
                    pointerControl = Pointer;
                    pointerControl.transform.position = existingCards[pointerAtIndex].pos;
                    inisiate = false;
                }


                if (Input.GetButtonDown("Submit"))
                {
                    if (!aCardIsZoomedIn)
                    {
                        aCardIsZoomedIn = true;
                        existingCards[pointerAtIndex].card.GetComponent<keyQuestCardScript>().playCardAnimation(pickUpAnimation);
                        pointerControl.active = false;
                    }
                    else if (aCardIsZoomedIn)
                    {
                        existingCards[pointerAtIndex].card.GetComponent<keyQuestCardScript>().playCardAnimation(putDownAnimation);
                        pointerControl.active = true;

                        aCardIsZoomedIn = false;
                    }
                }
                if (!aCardIsZoomedIn)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        pointerAtIndex++;
                        if (pointerAtIndex >= existingCards.Count - 1)
                        {
                            pointerAtIndex = existingCards.Count - 1;
                        }
                        pointerControl.transform.position = existingCards[pointerAtIndex].pos;
                        for(int i = 0; i < existingCards.Count; i++)
                        {
                            if(i == pointerAtIndex)
                            {
                                existingCards[i].card.transform.GetChild(1).GetComponent<SpriteRenderer>().material = pointerMaterial;
                            }
                            else
                            {
                                existingCards[i].card.transform.GetChild(1).GetComponent<SpriteRenderer>().material = defaultMaterial;
                            }
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        pointerAtIndex--;
                        if (pointerAtIndex <= 0)
                        {
                            pointerAtIndex = 0;
                        }
                        pointerControl.transform.position = existingCards[pointerAtIndex].pos;
                        for (int i = 0; i < existingCards.Count; i++)
                        {
                            if (i == pointerAtIndex)
                            {
                                existingCards[i].card.transform.GetChild(1).GetComponent<SpriteRenderer>().material = pointerMaterial;
                            }
                            else
                            {
                                existingCards[i].card.transform.GetChild(1).GetComponent<SpriteRenderer>().material = defaultMaterial;
                            }
                        }
                    }
                }

                if (Input.GetButtonDown("Cancel"))
                {
                    if (aCardIsZoomedIn)
                    {
                        existingCards[pointerAtIndex].card.GetComponent<keyQuestCardScript>().playCardAnimation(putDownAnimation);
                        pointerControl.active = true;

                        aCardIsZoomedIn = false;
                    }
                    else if (!aCardIsZoomedIn)
                    {
                        for (int i = 0; i < GOInTabbleOverlay.Count; i++)
                        {
                            Destroy(GOInTabbleOverlay[i]);
                        }
                        Destroy(pointerControl);
                        GOInTabbleOverlay.Clear();
                        existingCards.Clear();

                        tabbleState = TabbleState.atTabbleNotLooking;
                        inisiate = true;
                    }
                }
                break;

            case TabbleState.NotAtTabble:

                break;
        }
        if (tabbleState == TabbleState.atTabbleAndLooking)
        {
            PlayerMovement.canMove = false;
        }
    }
    #region seeIfAlexIsNearTheTabble
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (tabbleState == TabbleState.NotAtTabble)
        {
            tabbleState = TabbleState.atTabbleNotLooking;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (tabbleState == TabbleState.atTabbleNotLooking)
        {
            tabbleState = TabbleState.NotAtTabble;
        }
    }
    #endregion
    private void LateUpdate()
    {
        for (int i = 0; i < existingCards.Count; i++)
        {

            if (existingCards[i].card == existingCards[pointerAtIndex].card && aCardIsZoomedIn)
            {

                existingCards[i].card.transform.position =
                    Vector3.Lerp(
                         existingCards[i].card.transform.position,
                        cardGoTo.position,
                        travelTimeStamp);
            }
            else
            {
                existingCards[i].card.transform.position =
                    Vector3.Lerp(
                        existingCards[i].card.transform.position,
                        existingCards[i].pos,
                        travelTimeStamp);
            }
        }
    }
}
