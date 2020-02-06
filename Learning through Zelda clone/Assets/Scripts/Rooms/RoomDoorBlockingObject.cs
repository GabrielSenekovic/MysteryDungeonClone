using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoorBlockingObject : MonoBehaviour
{
    //Use this script to mask an entrance.
    //Set destroyprobability to 0.8
    //It has to be set two times, by two rooms on both sides
    public float DestroyProbability;
    private void Start()
    {
        float randomValue = Random.Range(0f, 1f);
        if(randomValue < DestroyProbability)
        {
            Destroy(gameObject);
        }
    }
}
