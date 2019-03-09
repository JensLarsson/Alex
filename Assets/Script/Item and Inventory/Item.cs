using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Alex/Item")]
public class Item : ScriptableObject
{

    public Sprite sprite;
    public bool deleteOnUse = true;
    public AudioClip useSound;
    [TextArea] public string description = "";
}
