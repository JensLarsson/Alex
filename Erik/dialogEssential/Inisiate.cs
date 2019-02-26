using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inisiate : MonoBehaviour {

    public void createObject()
    {
        GameObject newDia = Instantiate(gameObject, DialogManager.Instance.activeDialog.holder.transform.parent);
        newDia.name = gameObject.name;
    }

}
