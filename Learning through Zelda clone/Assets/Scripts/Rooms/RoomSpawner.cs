using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum openingDirection
{
    bottom,
    top,
    left,
    right,
}

public class RoomSpawner : MonoBehaviour
{
    public openingDirection OpeningDirection;
    // 1--> needs bottom door
    // 2--> needs top door
    // 3--> needs left door
    // 4--> needs right door

    // 5--> needs both bottom and top
    // 6--> needs both bottom and left
    // 7--> needs both bottom and right
    // 8--> needs both top and left
    // 9--> needs both top and right
    // 10--> needs both left and right

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 4f;

    public void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    public void Spawn()
    {
        GameObject newRoom = null;
        Debug.Log("Spawn called!");
        if (OpeningDirection == openingDirection.bottom)
        {
            rand = Random.Range(0, templates.bottomRooms.Length);
            newRoom = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = OpeningDirection;
            GetComponentInParent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
        }
        else if (OpeningDirection == openingDirection.top)
        {
            rand = Random.Range(0, templates.topRooms.Length);
            newRoom = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = OpeningDirection;
            GetComponentInParent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
        }
        else if (OpeningDirection == openingDirection.left)
        {
            rand = Random.Range(0, templates.leftRooms.Length);
            newRoom = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = OpeningDirection;
            GetComponentInParent<RoomParent>().hasRoomToRight = true;
            newRoom.GetComponent<RoomParent>().hasRoomToLeft = true;
        }
        else if (OpeningDirection == openingDirection.right)
        {
            rand = Random.Range(0, templates.rightRooms.Length);
            newRoom = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = OpeningDirection;
            GetComponentInParent<RoomParent>().hasRoomToLeft = true;
            newRoom.GetComponent<RoomParent>().hasRoomToRight = true;
        }
        newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = gameObject.transform.parent.GetComponent<RoomParent>();
        newRoom.GetComponent<RoomParent>().stepsAwayFromMain = gameObject.transform.parent.GetComponent<RoomParent>().stepsAwayFromMain + 1;
    }
    public void Destroy()
    {
        Debug.Log("Now destroying spawner of " + transform.parent.name);
        Destroy(gameObject);
    }
}
