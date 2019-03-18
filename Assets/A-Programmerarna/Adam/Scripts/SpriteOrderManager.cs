using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderManager : MonoBehaviour {

    public SpriteRenderer[] movingSprites;

    // Use this for initialization
    void Start () {
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
        List<SpriteRenderer> tempMovingSprites = new List<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.sortingLayerName == "Moving")
            {
                tempMovingSprites.Add(sprite);
            }
            if (sprite.sortingLayerName != "Background")
            {
                if (sprite.transform.childCount > 0)
                {
                    SpriteRenderer[] children = sprite.transform.GetComponentsInChildren<SpriteRenderer>();
                    for (int i = 1; i < children.Length; i++)
                    {
                        float yDiff = sprite.transform.position.y - children[i].transform.position.y;
                        if (yDiff <= 0)
                        {
                            children[i].sortingOrder += (int)(yDiff * -100f) + 2;
                        }
                    }
                }
                sprite.sortingLayerName = "Default2";
            }
            if (sprite.tag == "Door")
            {
                sprite.sortingOrder += (int)((sprite.transform.position.y - 0.5f) * -100f);
            }
            else
            {
                sprite.sortingOrder += (int)(sprite.transform.position.y * -100f);
            }

        }
        movingSprites = new SpriteRenderer[tempMovingSprites.Count];
        for (int i = 0; i < movingSprites.Length; i++)
        {
            movingSprites[i] = tempMovingSprites[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach (SpriteRenderer sprite in movingSprites)
        {
            sprite.sortingOrder = (int)(sprite.transform.position.y * -100f);
        }
    }
}
