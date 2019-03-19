using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectActivation
{
    public GameObject gObject;
    public bool active = true;

    public void initiate()
    {
        gObject.SetActive(active);
    }
}


public class FishingAnimationController : MonoBehaviour
{
    public GameObjectActivation first, removeBaseObject, bowl, playerIn, playerOut;

    private void Start()
    {
        PlayerTracker.Instance.transform.position = transform.position;
    }


    public void Exlamation()
    {
        first.initiate();
    }
    public void Base()
    {
        removeBaseObject.initiate();
    }
    public void DeleteBowl()
    {
        bowl.initiate();
    }
    public void PlayerIn()
    {
        playerIn.initiate();
    }
    public void PlayerOut()
    {
        playerOut.initiate();
    }
}
