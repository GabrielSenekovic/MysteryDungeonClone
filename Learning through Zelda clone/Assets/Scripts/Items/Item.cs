using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Icon/New Item", order = 1)]
public abstract class Item : ScriptableObject, IMoveable
{
    //This script will take care of the item in the inventory
    //I think I put this ON the item itself?
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int stackSize;
    private InventorySlots slot;

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }
    public int StackSize
    {
        get
        {
            return stackSize;
        }
    }
    public InventorySlots Slot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }

    public void Remove()
    {
        //Debug.Log("Removing " + name + " from the slot");
        if(Slot != null)
        {
            Slot.RemoveItem(this); //lets the item remove itself
        }
    }

}
