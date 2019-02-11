using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracking : MonoBehaviour
{
    public static List<GameObject> collisionList;
    
    private void Start()
    {
        collisionList  = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionList.Remove(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionList.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionList.Remove(collision.gameObject);
    }
}