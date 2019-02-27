using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Alex/Quest")]
public class QuestSO : ScriptableObject{

    public string _name;
    [TextArea]
    public string description;
}
