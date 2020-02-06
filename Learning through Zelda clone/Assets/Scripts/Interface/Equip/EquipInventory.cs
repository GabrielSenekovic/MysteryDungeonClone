using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInventory : MonoBehaviour, IUseable
{
    [SerializeField]
    private GameObject slotPrefab;

    [SerializeField]
    public int equipInventorySize;

    private List<InventorySlots> EquipSlotList = new List<InventorySlots>();

    public Sprite MyIcon
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    private InventorySlots fromSlot; //this is the slot items are picked up from

    public InventorySlots FromSlot
    {
        get
        {
            return fromSlot;
        }
        set
        {
            fromSlot = value;
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey; // grays out the icon that you picked up an item from
            }
        }
    }

    private void Awake()
    {
        Inventory.MyInstance.CountEquipables();
        equipInventorySize = Inventory.MyInstance.amountOfEquipables;
        RegulateSlots(equipInventorySize);
    }

    public void RegulateSlots(int slotCount)
    {
       // Debug.Log("Now regulating slots in equipinventory");
        int currentSlotAmount = EquipSlotList.Count;
        int amountOfSlotsToAdd = slotCount - currentSlotAmount;
        if (amountOfSlotsToAdd > 0)
        {
            for (int i = 0; i < amountOfSlotsToAdd; i++)
            {
                Debug.Log("Adding a slot to EquipInventory");
                InventorySlots slot = Instantiate(slotPrefab, transform).GetComponent<InventorySlots>();
                EquipSlotList.Add(slot);
            }
        } 
        else if (amountOfSlotsToAdd < 0)
        {
            for (int i = 0; i > amountOfSlotsToAdd; i--)
            {
                Debug.Log("Removing a slot to EquipInventory");
                InventorySlots slot = gameObject.GetComponentInChildren<InventorySlots>(); //I hope this works...
                if(slot.IsEmpty)
                {
                    EquipSlotList.Remove(slot);
                }
            }
        }
    }

    public void PlaceInEmpty(Item item)
    {
        Debug.Log("I have reached PlaceInEmpty in EquipInventory");
        if (CheckEmptySlots(item))
        {
            Debug.Log("Placing item in empty equipinventory slot");
            return;
        }
    }
    public bool CheckEmptySlots(Item item) //in tutorial, this was an additem function in the bag script
    {
        Debug.Log("CheckEmptySlots in equipinventory reached");
        foreach (InventorySlots slot in EquipSlotList)
        {
            //Checks if there's an empty slot
            if (slot.IsEmpty)
            {
                Debug.Log("There is an empty equipinventory slot");
                slot.AddItem(item);
            }
        }
        Debug.Log("There are no empty equipinventory slots");
        return false;
    }
    public bool PlaceInStack(Item item)
    {
        Debug.Log("Code has reached PlaceInStack in EquipInventory");
        foreach (InventorySlots equipSlots in EquipSlotList)
        {
            //Debug.Log("Slotlist has this many slots: " + slotList.Count);
            if (equipSlots.StackItem(item))
            {
                Debug.Log("StackItem returns true");
                return true;
            }
        }
        return false;
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

    public void DropItemUponClose()
    {
        if (Hand.MyInstance.MyMoveable != null)
        {
            Debug.Log("The hand is holding something");
            if (!EquipScreen.MyInstance.IsOpen)
            {
                Image icon = Hand.MyInstance.GetIcon();
                if (icon != null)
                {
                    if (fromSlot == null)
                    {
                        Debug.Log("fromSlot is null");
                    }
                    fromSlot.PutItemBack(); //This only makes the inventory icon white again
                    Hand.MyInstance.Drop(); //This makes moveable null
                    FromSlot = null; //This makes the fromslot null
                }
            }
        }
    }
}
