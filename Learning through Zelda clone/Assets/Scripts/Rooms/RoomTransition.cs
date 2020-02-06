using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour {
    public int Width;
    public int Height;
    public int X; //X and Y has to do with the location of the room on a room grid
    public int Y;
    public RoomParent MyRoomParent;

    private void OnDrawGizmos() //Oliver explains that gizmos helps you create scripts that can only be seen in the editor
    {
        Gizmos.color = Color.red; //shows room outline. Always the size of the room
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(new Vector3(X * Width, Y * Height, 0), new Vector3(2, 2, 0));
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
    // Debug.Log("Triggered the RoomTransition");
        if (RoomManager.Instance != null)
        {
          //  Debug.Log("Room Manager is here");
            if (MyCamera.Instance.CurrentRoom != MyRoomParent) //This should make sure that you dont transition into the transitioner while in the same room as it
            {
              //  Debug.Log(collider.name + "You are not in my room");
                if (collider.tag == "Player")
                {
                   // Debug.Log("Tag is Player");
                    RoomManager.MyInstance.OnPlayerEnterRoomTransition(this);
                    RoomParent roomParent = GetComponentInParent<RoomParent>();
                    RoomManager.MyInstance.OnPlayerEnterRoom(roomParent);
                }
            }
            
        }
    }
    public Vector3 GetCameraTransitionTarget()
    {
       // Debug.Log("I have the coordinates!");
        return transform.position;
        //return new Vector3(X * Width, Y * Height);
    }
}
