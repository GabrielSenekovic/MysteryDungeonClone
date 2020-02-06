using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Room(int inputX, int inputY)
    {
        xPosition = inputX;
        yPosition = inputY;
    }
    public int xPosition;
    public int yPosition;
}

public class RoomManager : MonoBehaviour {

    public static RoomManager Instance;
    public int debugBreak = 0;

    public int maxAmountOfRoomsThisTime;
    public static int maxAmountOfRooms;
    List<Room> roomPositions = new List<Room>();
    List<RoomSpawner> roomSpawners = new List<RoomSpawner>();
    List<RoomParent> deadEnds = new List<RoomParent>();
    List<RoomParent> deadEndsFurthestAway = new List<RoomParent>();
    public RoomParent bossRoom = null;
    int roomSpawner_Index = 0;

    public bool allRoomsSpawned = false;
    bool allSpawnersDeleted = false;

    public GameObject boss;
    public AudioClip fieldTheme;
    public AudioClip bossTheme;

    public static RoomManager MyInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = GameObject.FindObjectOfType<RoomManager>();
            }
            return Instance;
        }
    }

    private void Awake()
    {
        Instance = this;
        maxAmountOfRooms = maxAmountOfRoomsThisTime;
    }

    public void Update()
    {
        if(RoomIsSpawnable())
        {
            if(roomSpawners.Count == 0)
            {
                Debug.Log("There are no currently collected room spawners");
                GatherSpawners();
            }
            else
            {
                Debug.Log("There are collected room spawners, so it is time to spawn");
                SpawnRoom();
            }
        }
        else
        {
            if(allRoomsSpawned == true && allSpawnersDeleted == false)
            {
                DestroySpawners();
                CloseOffRoomEnds();
                GetComponent<TileReplacementManager>().UpdateAllTiles();
                FindEndRoomFurthestAway();
                SpawnBoss();
                AudioManager.ChangeBGM(fieldTheme);
            }
        }
    }

    public void OnPlayerEnterRoom(RoomParent roomParent)
    {
      //  Debug.Log("OnPlayerEnterRoom is being called");
        MyCamera.Instance.CurrentRoom = roomParent;
        MyCamera.Instance.m_IsUpdatingPosition = true; //this is the only code that sets this to true
      //  Debug.Log("Camera is updating position: " + MyCamera.Instance.m_IsUpdatingPosition);
    }

    public void OnPlayerEnterRoomTransition(RoomTransition roomTransition) 
    {
       // Debug.Log("Player is entering room transition");
        if(roomTransition != null)
        {
            MyCamera.Instance.RoomTransition = roomTransition;
           // Debug.Log("RoomTransition has been set as: " + roomTransition.name);
        }
    }

    public bool RoomIsSpawnable()
    {
        if (roomPositions.Count + 1 < maxAmountOfRooms)
        {
            Debug.Log("It is possible to spawn!");
            Debug.Log("There are currently " + (roomPositions.Count + 1) + " rooms and we need " + maxAmountOfRooms);
            return true;
        }
        else
        {
            allRoomsSpawned = true;
            return false;
        }
    }

    public void GatherSpawners()
    {
        Debug.Log("Gathering new spawners!");
        roomSpawner_Index = 0;
        RoomSpawner[] currentSpawners = FindObjectsOfType<RoomSpawner>();
        foreach(RoomSpawner spawner in currentSpawners)
        {
            roomSpawners.Add(spawner);
        }
    }

    public void SpawnRoom()
    {
        RoomSpawner spawner = roomSpawners[roomSpawner_Index];
        if (roomPositions.Exists(room => room.xPosition == spawner.transform.position.x && room.yPosition == spawner.transform.position.y)
            || (spawner.transform.position.x == 0 && spawner.transform.position.y == 0))
        {
            Debug.Log("Position of the spawner is: " + spawner.transform.position.x + "," + spawner.transform.position.y);
            Debug.Log("This Spawner was trying to spawn where there was already a room!");
        }
        else
        {
            Debug.Log("There are not enough rooms!");
            spawner.Spawn();
            roomPositions.Add(new Room((int)spawner.transform.position.x, (int)spawner.transform.position.y));
        }
        roomSpawner_Index++;
        spawner.Destroy();
        Debug.Log("The spawner index to get is: " + roomSpawner_Index);
        if (roomSpawner_Index == roomSpawners.Count)
        {
            roomSpawners.Clear();
        }
    }

    public void DestroySpawners()
    {
        RoomSpawner[] currentSpawners = FindObjectsOfType<RoomSpawner>();
        foreach (RoomSpawner spawner in currentSpawners)
        {
            Debug.Log("Now destroying all spawners!");
            spawner.Destroy();
        }
        allSpawnersDeleted = true;
    }

    public void CloseOffRoomEnds()
    {
        RoomParent[] currentRooms = FindObjectsOfType<RoomParent>();
        foreach(RoomParent room in currentRooms)
        {
            room.UpdateSelf();
        }
    }

    public void FindEndRoomFurthestAway()
    {
        RoomParent[] currentRooms = FindObjectsOfType<RoomParent>();
        foreach(RoomParent room in currentRooms)
        {
            if(room.IsEndRoom())
            {
                deadEnds.Add(room);
            }
        }
        int furthestAway = 0;
        foreach(RoomParent deadEnd in deadEnds)
        {
            if(deadEnd.stepsAwayFromMain > furthestAway)
            {
                furthestAway = deadEnd.stepsAwayFromMain;
            }
        }
        deadEndsFurthestAway.AddRange(deadEnds.FindAll(deadEnd => deadEnd.stepsAwayFromMain == furthestAway));
        if(deadEndsFurthestAway.Count > 1)
        {
            ChooseBossRoom();
        }
        else
        {
            bossRoom = deadEndsFurthestAway[0];
        }
    }

    public void ChooseBossRoom()
    {
        System.Random r = new System.Random();
        bossRoom = deadEndsFurthestAway[r.Next(0, deadEndsFurthestAway.Count-1)];
    }
    
    public void SpawnBoss()
    {
        boss.GetComponent<CharacterEnemyGeneralControl>().myRoom = bossRoom;
        Instantiate(boss, bossRoom.transform.position, Quaternion.identity);
    }
}
