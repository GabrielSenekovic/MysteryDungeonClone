using UnityEngine;
using System.Collections;

public class InteractableBase : MonoBehaviour
    //Seems all that this script does it act as base for all other interactable scripts. It also grabs the interactionmodel of the playable character
{
    public bool IsInteractable;
    virtual public void OnInteract(Character character) //a virtual method means a subclass can overwrite it
    {
        Debug.LogWarning("OnInteract is not implemented");
    }
    virtual public void OnStopInteract()
    {

    }

    CharacterInteractionModel FindCharacterInteractionModel()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
        {
            Debug.LogWarning("No player found");
            return null;
        }

        CharacterInteractionModel interactionModel = playerObject.GetComponent<CharacterInteractionModel>(); //Here the scripts grabs the interactionmodel of the playable character
        if (interactionModel == null)
        {
            Debug.LogWarning("No interactionmodel found on player");
            return null;
        }

        return interactionModel;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0));
    }

}