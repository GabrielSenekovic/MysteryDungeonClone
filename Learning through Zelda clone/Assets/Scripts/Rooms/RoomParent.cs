using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomParent : MonoBehaviour
{

    public int Width;
    public int Height;
    public int X; //X and Y has to do with the location of the room on a room grid
    public int Y;
    public Vector3 maxCameraPosition;
    public Vector3 minCameraPosition;

    public openingDirection directionSpawnedFrom;

    public bool hasRoomToLeft;
    public bool hasRoomToRight;
    public bool hasRoomToNorth;
    public bool hasRoomToSouth;

    public string previousRoom;
    public RoomParent roomThatSpawnedMe;

    public bool isMainRoom;
    private bool isDeadEnd = false;

    public int stepsAwayFromMain = 0;

    RoomSpawner[] mySpawners;

    private void Awake()
    {
        maxCameraPosition = new Vector3(transform.position.x + ((Width / 2) - 10), transform.position.y + ((Height / 2) - (11 / 2)), 0);
        minCameraPosition = new Vector3(transform.position.x - ((Width / 2) - 10), transform.position.y - ((Height / 2) - (11 / 2)), 0);
        mySpawners = GetComponentsInChildren<RoomSpawner>();
    }
    void Start()
    {
        if (EternalRoomManager.Instance == null) //I should be able to use this script on a scene without the RoomManager, because the RoomManager scripts are unreachable without it
        {
           // Debug.LogWarning("This room is not an eternal room");
            return;
        }
        EternalRoomManager.Instance.RegisterRoom(this);
    }

    private void OnDrawGizmos() //Oliver explains that gizmos helps you create scripts that can only be seen in the editor
    {
        Gizmos.color = Color.red; //shows room outline. Always the size of the room
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
        Gizmos.color = Color.green; // Displays top right corner of room (yes!)
        Gizmos.DrawWireCube(new Vector3(transform.position.x + ((Width / 2) -10), transform.position.y + ((Height / 2) - (11 / 2)), 0), new Vector3(20, 11, 0));
        Gizmos.DrawWireCube(new Vector3(transform.position.x - ((Width / 2) - 10), transform.position.y - ((Height / 2) - (11 / 2)), 0), new Vector3(20, 11, 0));
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(new Vector3(X * Width, Y * Height, 0), new Vector3(20, 11, 0));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (EternalRoomManager.Instance != null)
        {
            if (collider.tag == "Player")
            {
                EternalRoomManager.Instance.OnPlayerEnterRoom(this);
            }
        }

        if(RoomManager.Instance!= null) //You dont have to check for the transitions!!! The parentobject doesnt have a collider2d!!!
        {
            Debug.Log("There is a Room Manager");
            if (collider.tag == "Player")
            {
                RoomManager.Instance.OnPlayerEnterRoom(this);
            }
        }
       
    }
    public void UpdateSelf()
    {
        RoomTemplates templates = RoomManager.MyInstance.GetComponent<RoomTemplates>();
        int rand;
        if (hasRoomToNorth == true & hasRoomToSouth == true & hasRoomToRight == true & hasRoomToLeft == true)
        {
            isDeadEnd = true;
        }
        if (hasRoomToNorth == true & hasRoomToSouth == false & hasRoomToRight == false & hasRoomToLeft == false)
        {
            rand = Random.Range(0, templates.topEndRooms.Length);
            GameObject newRoom = Instantiate(templates.topEndRooms[rand], transform.position, templates.topEndRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().isDeadEnd = true;
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == false & hasRoomToSouth == true & hasRoomToRight == false & hasRoomToLeft == false)
        {
            rand = Random.Range(0, templates.bottomEndRooms.Length);
            GameObject newRoom = Instantiate(templates.bottomEndRooms[rand], transform.position, templates.bottomEndRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().isDeadEnd = true;
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == false & hasRoomToSouth == false & hasRoomToRight == true & hasRoomToLeft == false)
        {
            rand = Random.Range(0, templates.rightEndRooms.Length);
            GameObject newRoom = Instantiate(templates.rightEndRooms[rand], transform.position, templates.rightEndRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().isDeadEnd = true;
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToRight = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == false & hasRoomToSouth == false & hasRoomToRight == false & hasRoomToLeft == true)
        {
            rand = Random.Range(0, templates.leftEndRooms.Length);
            GameObject newRoom = Instantiate(templates.leftEndRooms[rand], transform.position, templates.leftEndRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().isDeadEnd = true;
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToLeft = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == true & hasRoomToSouth == true & hasRoomToRight == false & hasRoomToLeft == false)
        {
            rand = Random.Range(0, templates.TBRooms.Length);
            GameObject newRoom = Instantiate(templates.TBRooms[rand], transform.position, templates.TBRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == true & hasRoomToSouth == false & hasRoomToRight == true & hasRoomToLeft == false)
        {
            rand = Random.Range(0, templates.TRRooms.Length);
            GameObject newRoom = Instantiate(templates.TRRooms[rand], transform.position, templates.TRRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToRight = true;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == true & hasRoomToSouth == false & hasRoomToRight == false & hasRoomToLeft == true)
        {
            rand = Random.Range(0, templates.TLRooms.Length);
            GameObject newRoom = Instantiate(templates.TLRooms[rand], transform.position, templates.TLRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToLeft = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == true & hasRoomToSouth == true & hasRoomToRight == true & hasRoomToLeft == false)
        {
            rand = Random.Range(0, templates.TBRRooms.Length);
            GameObject newRoom = Instantiate(templates.TBRRooms[rand], transform.position, templates.TBRRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToRight = true;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == true & hasRoomToSouth == true & hasRoomToRight == false & hasRoomToLeft == true)
        {
            rand = Random.Range(0, templates.TBLRooms.Length);
            GameObject newRoom = Instantiate(templates.TBLRooms[rand], transform.position, templates.TBLRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToLeft = true;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == true & hasRoomToSouth == false & hasRoomToRight == true & hasRoomToLeft == true)
        {
            rand = Random.Range(0, templates.TRLRooms.Length);
            GameObject newRoom = Instantiate(templates.TRLRooms[rand], transform.position, templates.TRLRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToLeft = true;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToRight = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);

        }
        if (hasRoomToNorth == false & hasRoomToSouth == true & hasRoomToRight == true & hasRoomToLeft == false)
        {
            rand = Random.Range(0, templates.BRRooms.Length);
            GameObject newRoom = Instantiate(templates.BRRooms[rand], transform.position, templates.BRRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToRight = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == false & hasRoomToSouth == true & hasRoomToRight == false & hasRoomToLeft == true)
        {
            rand = Random.Range(0, templates.BLRooms.Length);
            GameObject newRoom = Instantiate(templates.BLRooms[rand], transform.position, templates.BLRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToLeft = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == false & hasRoomToSouth == true & hasRoomToRight == true & hasRoomToLeft == true)
        {
            rand = Random.Range(0, templates.BRLRooms.Length);
            GameObject newRoom = Instantiate(templates.BRLRooms[rand], transform.position, templates.BRLRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToLeft = true;
            newRoom.GetComponent<RoomParent>().hasRoomToRight = true;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
        if (hasRoomToNorth == false & hasRoomToSouth == false & hasRoomToRight == true & hasRoomToLeft == true)
        {
            rand = Random.Range(0, templates.RLRooms.Length);
            GameObject newRoom = Instantiate(templates.RLRooms[rand], transform.position, templates.RLRooms[rand].transform.rotation);
            newRoom.GetComponent<RoomParent>().directionSpawnedFrom = directionSpawnedFrom;
            newRoom.GetComponent<RoomParent>().hasRoomToNorth = true;
            newRoom.GetComponent<RoomParent>().hasRoomToSouth = true;
            newRoom.GetComponent<RoomParent>().previousRoom = gameObject.name;
            newRoom.GetComponent<RoomParent>().roomThatSpawnedMe = roomThatSpawnedMe;
            newRoom.GetComponent<RoomParent>().stepsAwayFromMain = stepsAwayFromMain;
            Destroy(gameObject);
        }
    }

    public bool IsEndRoom()
    {
        if(isDeadEnd)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
