using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    public static RoomTemplates Instance;

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject[] TRRooms;
    public GameObject[] TLRooms;
    public GameObject[] BLRooms;
    public GameObject[] BRRooms;
    public GameObject[] TBRooms;
    public GameObject[] RLRooms;

    public GameObject[] bottomEndRooms;
    public GameObject[] topEndRooms;
    public GameObject[] leftEndRooms;
    public GameObject[] rightEndRooms;

    public GameObject[] BRLRooms;
    public GameObject[] TBRRooms;
    public GameObject[] TRLRooms;
    public GameObject[] TBLRooms;

    [SerializeField]
    public RoomParent[] allRooms;

    public float waitTime;
    private bool spawnedBoss;

    public bool roomsUpdated;

    public void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        /*if (waitTime <= 0 && spawnedBoss == false)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                if(i == rooms.Count-1)
                {
                    RoomParent bossRoomParent = rooms[i].GetComponent<RoomParent>();
                    RoomManager.MyInstance.SpawnBoss(rooms[i].transform.position, bossRoomParent);
                    spawnedBoss = true;
                }
            }
        }
        else if(spawnedBoss == false)
        {
            waitTime -= Time.deltaTime;
        }
        else if(spawnedBoss == true)
        {
            waitTime = 0;
        }
        if(spawnedBoss == true && rooms.Count >= maxAmountOfRooms && roomsUpdated == false)
        {
            //The boss is spawned, room is more than or equal to how many there are supposed to be, and the rooms havent been updated yet
            int i = 0; //prevents all tilemaps from updating
            allRooms = FindObjectsOfType(typeof(RoomParent)) as RoomParent[];
            foreach (RoomParent room in allRooms)
            {
                if (room.isFinal == false)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                Debug.Log("The code gets this far");
                GetComponent<TileReplacementManager>().UpdateAllTiles();
                roomsUpdated = true;
            }
            else
            {
                Debug.Log("The rooms arent finished yet");
            }
        }
        */
    }
    /*
     * for(int y = 0; y < roomHeightInCells; y++)
{
    for(int x = 0; x < roomWidthInCells; x++)
    {
        //spawn room at coordinates[x][y]
    }

}
if (room[xarray][yarray + 1] == true)
{
    //put door at top
}*/
    
    //use something like this instead please god
}
