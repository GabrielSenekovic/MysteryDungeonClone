using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : InteractableBase
{
    public Dialog myDialog;
    public override void OnInteract(Character character)
    {
        Debug.Log("Interacting with NPC");
        DialogManager.OnSpeak(myDialog);
    }
}
