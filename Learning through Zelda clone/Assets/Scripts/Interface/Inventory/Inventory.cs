using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IUseable {
    //This script will be used as a depository for the character inventorymodel
    private int slots;
    [SerializeField]
    private GameObject slotPrefab;

    private CanvasGroup canvasGroup;

    private List<InventorySlots> slotList = new List<InventorySlots>();

    public Dictionary<ItemType, int> m_Items = new Dictionary<ItemType, int>(); // this dictionary is our item. Basically only used for money

    public int inventorySize;
    public int amountOfEquipables;

    [SerializeField]
    public int equipInventorySize;

    private static Inventory instance;

    public EquipInventory equipInventory;

    public static Inventory MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Inventory>();
            }
            return instance;
        }
    }

    public int Slots
    {
        get
        {
            return slots;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public void Initialize(int slots)
    {
        this.slots = slots;
    }
    public void Use()
    {
        AddSlots(slots);
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
            if(value != null)
            {
                fromSlot.MyIcon.color = Color.grey; // grays out the icon that you picked up an item from
            }
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventorySize = 42;
        Initialize(inventorySize);
        Use();
    }

    private void Update()
    {
    }

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            InventorySlots slot = Instantiate(slotPrefab, transform).GetComponent<InventorySlots>();
            slotList.Add(slot);
        }
    }
    public void FindItemBeforeAdd(ItemType itemType)
    {
        ItemData itemData = Database.Item.FindItem(itemType);
        Debug.Log("The Inventory has found a " + itemData.name);
        Item item = itemData.Item;
        Debug.Log("It seems to be a " + item.name);
        AddItem(item);
    }
    public void AddItem(Item item)
    {
        Debug.Log("The " + item.name + " has reached the Inventory's AddItem function");
        if (item.StackSize > 0)
        {
            if(PlaceInStack(item)) //Here it actually goes to PlaceInStack() further down
            {
                Debug.Log("PlaceInStack has returned true");
                return;
            }
        }
        Debug.Log("This item doesn't already exist in inventory");
        PlaceInEmpty(item);
    }

    private void PlaceInEmpty(Item item)
    {
        Debug.Log("I have reached PlaceInEmpty");
        if (CheckEmptySlots(item))
        {
            Debug.Log("Placing item in empty slot");
            return;
        }
    }
    public bool CheckEmptySlots(Item item) //in tutorial, this was an additem function in the bag script
    {
        Debug.Log("CheckEmptySlots in Inventory reached");
        foreach(InventorySlots slot in slotList)
        {
            //Checks if there's an empty slot
            if(slot.IsEmpty)
            {
                Debug.Log("There is an empty slot for " + item); 
                slot.AddItem(item);

                if (item is WeaponItem && Hand.MyInstance.MyMoveable == null || item is ArmorItem && Hand.MyInstance.MyMoveable == null)
                    //This should ONLY be excecuted if the item is put here from external sources.
                    //If the item is added from the equipscreen, then this shouldn't occur.
                    //Because otherwise the item will duplicate itself if I put it back into the equipinventory.
                    //Lets see if it changes if I check for the hands moveable
                {
                    Debug.Log("In CheckEmptySlots, looking to see if the item is an equipable");
                    CountEquipables();
                    Debug.Log("CheckEmptySlots have counted the equipables");
                    equipInventorySize = MyInstance.amountOfEquipables;
                    equipInventory.RegulateSlots(equipInventorySize);
                    equipInventory.PlaceInEmpty(item);
                }
                return true;
            }
        }
        Debug.Log("There are no empty slots");
        return false;
    }

    private bool PlaceInStack(Item item)
    {
        Debug.Log("Code has reached PlaceInStack");
        foreach (InventorySlots slots in slotList)
        {
            //Debug.Log("Slotlist has this many slots: " + slotList.Count);
            if(slots.StackItem(item))
            {
                Debug.Log("StackItem returns true");

                if(equipInventory == null)
                {
                    Debug.Log("There is no equipinventory attached to this Inventory gameobject");
                }

                if (item is WeaponItem || item is ArmorItem)
                {
                    CountEquipables(); //Make it so that it doesnt have to be called in update somehow
                    equipInventorySize = Inventory.MyInstance.amountOfEquipables;
                    equipInventory.RegulateSlots(equipInventorySize);
                    equipInventory.PlaceInStack(item);
                }
                return true;
            }
        }
        return false;
    }


    [SerializeField]
    private Item[] items;

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;

        DropItemUponClose();
    }

    public void CountEquipables() 
    {
        amountOfEquipables = 0;
        foreach (InventorySlots slot in slotList)
        {
            //Checks if there's an empty slot
            Item item = slot.MyItem;
            if (item is WeaponItem || item is ArmorItem)
            {
                amountOfEquipables += 1;
            }
        }
       // Debug.Log("The amount of equipables is: " + amountOfEquipables);
    }

    public void DropItemUponClose()
    {
        if (Hand.MyInstance.MyMoveable != null)
        {
            Debug.Log("The hand is holding something");
            if (!IsOpen)
            {
                Image icon = Hand.MyInstance.GetIcon();
                if (icon != null)
                {
                    if(fromSlot == null)
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
    public void FindItemInInventoryAndRemove(Item item)
    {
        //called from EquipScreen and other screens where inventory items may show up
        //This is for those screens to find the item that corresponds to the item that is in their inventory
        //So it finds the item in its slot and finds the item in a slot in the inventory then removes it
        foreach (InventorySlots slots in slotList)
        {
            InventorySlots thisSlot = slots.GetComponent<InventorySlots>();
            if(thisSlot == null)
            {
               // Debug.Log("thisSlot is null");
            }
           // Debug.Log("thisSlot comes from: " + thisSlot.transform.parent.name);
                if (thisSlot.MyItem == item)
                {
                Debug.Log("The item that was in this slot has been found in the inventory and will be removed now");
                thisSlot.RemoveItem(item);
                break;
                }
        }
    }
}
