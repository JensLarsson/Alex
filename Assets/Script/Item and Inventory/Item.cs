using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Alex/Item")]
public class Item : ScriptableObject {

    public Sprite sprite;
    [TextArea] public string description="";
}
