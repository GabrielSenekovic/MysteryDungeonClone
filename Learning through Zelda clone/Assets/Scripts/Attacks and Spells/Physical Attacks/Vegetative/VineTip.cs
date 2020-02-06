using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTip : MonoBehaviour
{
    private bool hasCollided = false;
    private GameObject lastCollidedWith = null;

    public void OnCollisionEnter2D(Collision2D collider)
    {
        lastCollidedWith = collider.gameObject;
        Debug.Log("I have now collided");
        hasCollided = true;
    }

    public bool CheckIfCollided()
    {
        return hasCollided;
    }

    public GameObject CheckLastCollided()
    {
         return lastCollidedWith;
    }

    public void Reset()
    {
        hasCollided = false;
        lastCollidedWith = null;
    }

}
