using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public static MyCamera Instance;
    public Transform target; //Transform is only an x and a y position
    public CharacterMovementModel freezeCharacter; //This might fuck up later, when I instantiate characters. The camera has to find the character on its own.
    public float smoothing; //I dont like this variable. I think I will only use it if character is droozy
    public RoomParent CurrentRoom;
    public RoomTransition RoomTransition;
    public bool m_IsUpdatingPosition;
    [SerializeField]
    private Vector3 maxPosition;
    private Vector3 minPosition;
    public float MovementSpeedOnRoomChange;

    private void Awake()
    {
        Instance = this;
        m_IsUpdatingPosition = false;
    }

    private void Start()
    {
        if(CurrentRoom != null)
        {
            maxPosition = CurrentRoom.maxCameraPosition;
            minPosition = CurrentRoom.minCameraPosition;
        }
    }
    private void Update()
    {
            UpdatePosition();
    }
    private void LateUpdate() //Is always updated last. If the camera moves in update, it may move before the character
    {
        if(CurrentRoom != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z); //The target position is set to the position of the player
                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
                if (m_IsUpdatingPosition == false)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
                }
            }
        }
        else
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        }
    }
    void UpdatePosition() 
    //This should set m_IsUpdatingPosition to true, then to false when it has changed scenes
    //Yeah this is the change scenes thing. It will get its variables from another script that will call upon it.
    {

        if (CurrentRoom == null)
        {
            return;
        }
        if(m_IsUpdatingPosition == true)
        {
            //Debug.Log("UpdatePosition is being called");
            Vector3 targetPosition = new Vector3();
            if(RoomTransition == null)
            {
                //Debug.Log("Room transition was null, getting center of this room");
                targetPosition = CurrentRoom.GetRoomCenter();
                //Debug.Log("Seems like RoomTransition was null!");
            }
            if (RoomTransition != null)
            {
              //  Debug.Log("Room transition wasnt null, getting transition target");
                targetPosition = RoomTransition.GetCameraTransitionTarget();//Here it should get the center of the roomtransition collider
                //Debug.Log("Target is set to RoomTransition coordinates: " + targetPosition.x +" and "+ targetPosition.y);
            }
            targetPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * MovementSpeedOnRoomChange);
            maxPosition = CurrentRoom.maxCameraPosition;
            minPosition = CurrentRoom.minCameraPosition;

          //  Debug.Log("transform position is: " + transform.position + " and target position is: " + targetPosition);

            while (transform.position != targetPosition)
                {
                //if it does not freeze the character, the code things that you are already at the targetposition
               // Debug.Log("Code freezes character");
                    freezeCharacter.SetFrozen(true,true,false);
                    return;
                }
          //  Debug.Log("It is here it is set to false");
            m_IsUpdatingPosition = false;
            RoomTransition = null;
            freezeCharacter.SetFrozen(false,false, false);
        }
        
    }
}
