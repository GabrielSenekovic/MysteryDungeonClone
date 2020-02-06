using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomData
{
    public string Name;
    public int X;
    public int Y;
}
public class EternalRoomManager : MonoBehaviour
{
    public static EternalRoomManager Instance;
    string m_CurrentWorldName = "EternalForest";
    RoomData m_CurrentLoadRoomData;
    Queue<RoomData> m_LoadRoomQueue = new Queue<RoomData>();
    List<RoomParent> m_LoadedRooms = new List<RoomParent>();
    bool m_IsLoadingRoom = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadRoom("MainRoom",0, 0);
    }
    private void Update()
    {
        UpdateRoomQueue();
    }
    void UpdateRoomQueue()
    {
        if(m_IsLoadingRoom == true)
        {
            return;
        }
        if(m_LoadRoomQueue.Count == 0)
        {
            return;
        }
        m_CurrentLoadRoomData = m_LoadRoomQueue.Dequeue();   //Removes first element of queue
        m_IsLoadingRoom = true;
        Debug.Log("Loading new room: " + m_CurrentLoadRoomData.Name + " at " + m_CurrentLoadRoomData.X + ", " + m_CurrentLoadRoomData.Y);
        StartCoroutine(LoadRoomRoutine(m_CurrentLoadRoomData));
    }

    void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x,y)==true)
        {
            return;
        }
        RoomData newRoomData = new RoomData();
        newRoomData.Name = name;
        newRoomData.X = x;
        newRoomData.Y = y;
        m_LoadRoomQueue.Enqueue(newRoomData);
    }
    IEnumerator LoadRoomRoutine(RoomData data)
    {
        string levelName = m_CurrentWorldName + data.Name;
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        //Application.LoadLevelAdditiveAsync(m_CurrentWorldName + name);
        //LoadLevel just changes current level to another
        //LoadLevelAdditive loads another level on your level
        //LoadLevelAdditiveAsync does that in the background
        //Altho Unity told me Application.LoadLevelAdditiveAsync is obsolete
        //Tells me to use "Use SceneManager.LoadSceneAsync"
        while(loadLevel.isDone == false)
        {
            Debug.Log("Loading " + levelName + ": " + Mathf.Round(loadLevel.progress * 100) + "%");
            yield return null;
        }

    }

    public void RegisterRoom(RoomParent roomParent)
    {
        roomParent.transform.position = new Vector3(m_CurrentLoadRoomData.X * roomParent.Width, m_CurrentLoadRoomData.Y * roomParent.Height, 0);
        roomParent.X = m_CurrentLoadRoomData.X;
        roomParent.Y = m_CurrentLoadRoomData.Y;
        roomParent.name = m_CurrentWorldName + "-" + m_CurrentLoadRoomData.Name + "-" + roomParent.X + "-" + roomParent.Y;
        m_IsLoadingRoom = false;
        if(m_LoadedRooms.Count == 0)
        {
            GameCamera.Instance.CurrentRoom = roomParent;
        }
        m_LoadedRooms.Add(roomParent);
    }

    bool DoesRoomExist(int x, int y)
    {
        /*This is something called a lambda expression
         * within the parenthesis are the conditions for the item to be found
         * find item that has an X position equal to x and Y equal to y
         */
        return m_LoadedRooms.Find(item => item.X == x && item.Y == y) != null;
        //if this returns null, then we dont have a room
    }

    string GetRandomRegularRoomName()
    {
        string[] possibleRooms = new string[] { "SwordRoom", "ChiropteradheiRoom" };
        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(RoomParent roomParent)
    {
        //so the +1 and -1 here decides how far away from the current room the next room should spawn
        //Im pretty sure that if I turn those into variables, I will be able to inherit them from the RoomParent
        //And make bigger rooms
        GameCamera.Instance.CurrentRoom = roomParent;
        if(DoesRoomExist(roomParent.X+1,roomParent.Y) == false)
        {
            LoadRoom(GetRandomRegularRoomName(), roomParent.X + 1, roomParent.Y);
        }
        if (DoesRoomExist(roomParent.X - 1, roomParent.Y) == false)
        {
            LoadRoom(GetRandomRegularRoomName(), roomParent.X - 1, roomParent.Y);
        }
        if (DoesRoomExist(roomParent.X, roomParent.Y +1) == false)
        {
            LoadRoom(GetRandomRegularRoomName(), roomParent.X, roomParent.Y+1);
        }
        if (DoesRoomExist(roomParent.X, roomParent.Y -1) == false)
        {
            LoadRoom(GetRandomRegularRoomName(), roomParent.X, roomParent.Y-1);
        }
    }
}
