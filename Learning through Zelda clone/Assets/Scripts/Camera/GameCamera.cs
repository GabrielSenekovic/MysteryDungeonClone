using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera Instance;
    public RoomParent CurrentRoom;
    public float MovementSpeedOnRoomChange;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        UpdatePosition();
        /*Okay so right now it always checks for the next room. I want this function to be called on
         * called on by another script!
    */
    }
    void UpdatePosition()
    {
        if(CurrentRoom == null)
        {
            return;
        }
        Vector3 targetPosition = CurrentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * MovementSpeedOnRoomChange);
    }

}
