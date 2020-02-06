using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : InteractableBase {

    public float ThrowDistance = 5;
    public float ThrowSpeed = 3;
    Vector3 m_CharacterThrowPosition;
    Vector3 m_ThrowDirection;
    public void Awake()
    {
        IsInteractable = true;
    }
    public override void OnInteract(Character character)
    {
        CharacterInteractionModel interactionModel = character.GetComponent<CharacterInteractionModel>();
        if( interactionModel == null)
        {
            return;
        }

        BroadcastMessage("OnPickupObject", character, SendMessageOptions.DontRequireReceiver);

        interactionModel.PickupObject(this);
    }
    public void Throw(Character character)
    {
        StartCoroutine(ThrowRoutine(character.transform.position, Character.Instance.Movement.GetFacingDirection()));
    }
    IEnumerator ThrowRoutine(Vector3 characterThrowPosition, Vector3 throwDirection)
    {
        transform.parent = null;
        Vector3 targetPosition = characterThrowPosition + throwDirection.normalized * ThrowDistance;
        while(transform.position != targetPosition) 
            /*as long as the position of this gameobject isnt on the target, this script will excecute
            */
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, ThrowSpeed * Time.deltaTime);
            yield return null;
        }
        BroadcastMessage("OnObjectThrown", SendMessageOptions.DontRequireReceiver);
    }
}
