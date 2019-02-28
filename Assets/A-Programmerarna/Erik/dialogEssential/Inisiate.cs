using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inisiate : MonoBehaviour {

    public void createObject()
    {
        if (!DialogManager.Instance.activeDialog.holder.GetComponent<ContaningDialog>().hasBeenRead)
        {
            GameObject newDia = Instantiate(gameObject, DialogManager.Instance.activeDialog.holder.transform.parent);
            newDia.name = gameObject.name;
        }
    }

}
