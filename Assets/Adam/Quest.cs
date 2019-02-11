using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest {

    public string _name;
    [TextArea]
    public string description;
	public string person;
    public bool hidden = false;
}