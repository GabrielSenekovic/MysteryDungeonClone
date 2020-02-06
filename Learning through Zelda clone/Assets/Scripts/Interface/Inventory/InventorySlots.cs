using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour, IPointerClickHandler, IClickable
{
    //were gonna use a new collection that is neither array, list or dictionary, called a stack
    private ObservableStack<Item> itemStack = new ObservableStack<Item>();
    //when you add things to a stack, its called "to push"
    //here it becomes clear that this tutorials "item" corresponds to "itemdata"

    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text stackSize;

    public bool IsEmpty
    {
        get
        {
            return itemStack.Count == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            if(IsEmpty || MyCount < MyItem.StackSize)
            {
                return false;
            }
            return true;
        }
    }

    public Item MyItem
    {
        
        get
        {
            if(!IsEmpty)
            {

                return itemStack.Peek();
            }
            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return itemStack.Count;
        }
    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public void Awake()
    {
        itemStack.OnPop += new UpdateStackEvent(UpdateSlot);
        itemStack.OnPush += new UpdateStackEvent(UpdateSlot);
        itemStack.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool AddItem(Item item)
    {
        Debug.Log("Code has reached the slots AddItem function with the item: " + item);
        itemStack.Push(item); //item is added to stack
        icon.sprite = item.MyIcon; //Now the icon of the image on the slot is set to the icon referenced in the item
        icon.color = Color.white; //To make the icon not transparent
        item.Slot = this;
        Debug.Log("Now the item in this slot is a:" + MyItem);
        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if(IsEmpty||newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for(int i = 0; i < count; i++)
            {
                if(IsFull)
                {
                    Debug.Log("The slot is full");
                    return false;
                }
                AddItem(newItems.Pop()); //puts all items in the slot if its not full
            }
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        if(!IsEmpty && item == MyItem) //I added this, because otherwise, for some reason, the inventoryslot wanted to remove the sword instead of the bow
        {
            //The item is being called from the Item script.
            //Debug.Log("The code has reached the slot to remove the item: " + item.name);
            //Debug.Log("In this slot there is a " + MyItem);
            itemStack.Pop(); //removes item 
        }
    }

    public void Clear()
    {
        if(itemStack.Count > 0)
        {
            Debug.Log("Clearing this slot");
            itemStack.Clear();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log("This slot has been clicked");
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("You clicked with your left button");
            if(Inventory.MyInstance.FromSlot == null && !IsEmpty)//if we dont have something to move
            {
                //You have not picked an item up from the inventory and this slot has something in it.
                if(Hand.MyInstance.MyMoveable != null)
                {
                    Debug.Log("If this shows up, then you are moving an item from the equipscreen into the inventory");
                    //If the hand has something on it anyway, then you have picked it from the equipscreen
                    //So this section here deals with what happens when you pull an equipped item back into the inventory, but only if it swaps places with something
                    if (Hand.MyInstance.MyMoveable is ArmorItem)
                    {
                        if(MyItem is ArmorItem && (MyItem as ArmorItem).MyEquipPosition == (Hand.MyInstance.MyMoveable as ArmorItem).MyEquipPosition)
                        {
                            Debug.Log("MyItem is: " + MyItem);
                            //If the item in this slot is the same type of item, then they will swap places
                            if(MyItem != Hand.MyInstance.MyMoveable as Item)
                            {
                                Debug.Log("These two items are not the same");
                                Inventory.MyInstance.FindItemInInventoryAndRemove(MyItem);
                                (MyItem as ArmorItem).Equip();//this eventually leads back to the equiparmor in equipbutton, which it should
                                Inventory.MyInstance.AddItem(Hand.MyInstance.MyMoveable as Item);
                                (Hand.MyInstance.MyMoveable as Item).Slot = this;//Makes sure i cant set the slot to the other inventory by accident
                            }
                            else
                            {
                                Debug.Log("These two items are the same");
                                (Hand.MyInstance.MyMoveable as Item).Slot = this;
                                (MyItem as ArmorItem).Equip();
                            }
                            Hand.MyInstance.Drop();
                        }
                       
                    }
                    if (Hand.MyInstance.MyMoveable is WeaponItem)
                    {
                        if (MyItem is WeaponItem && (MyItem as WeaponItem).MyEquipPosition == (Hand.MyInstance.MyMoveable as WeaponItem).MyEquipPosition)
                        {
                            Debug.Log("MyItem is: " + MyItem + ". MyItem is the item that is currently in this slot");
                            if(MyItem!= Hand.MyInstance.MyMoveable as Item)
                            {
                                Inventory.MyInstance.FindItemInInventoryAndRemove(MyItem); //This removes the item from the inventory. It works as intended
                                (MyItem as WeaponItem).Equip();
                                Debug.Log("MyItem is: " + MyItem + ". MyItem is the item that is currently in this slot");
                                //This switches places with the item you click on, meaning the item on the slot ends up on the equipbutton
                                //It leads into the item, which leads into the equipweapon function on the equipscreen
                                Debug.Log("The item in the hand is a: " + Hand.MyInstance.MyMoveable);
                                Inventory.MyInstance.AddItem(Hand.MyInstance.MyMoveable as Item);
                                (Hand.MyInstance.MyMoveable as Item).Slot = this; //This makes sure that I cant accidentally set the slot to the other inventory
                            }
                            else
                            {
                                (Hand.MyInstance.MyMoveable as Item).Slot = this;
                                (MyItem as WeaponItem).Equip();
                            }
                           
                            Hand.MyInstance.Drop();
                        }
                    }
                }
                else
                {
                    //In this case, the hand is empty, so you are free to pick shit up
                    Debug.Log("Taking the item: " + MyItem.name);
                    Hand.MyInstance.TakeMoveable(MyItem as IMoveable);
                    (Hand.MyInstance.MyMoveable as Item).Slot = this; //this just updates what the slot is
                    Inventory.MyInstance.FromSlot = this;
                    Inventory.MyInstance.equipInventory.FromSlot = this;
                }
                
            }
            else if(Inventory.MyInstance.FromSlot == null && IsEmpty)
            {
                //This also cannot be executed if you have something moving, but in this case the slot IS empty.
                if(Hand.MyInstance.MyMoveable is ArmorItem)
                {
                    ArmorItem armor = (ArmorItem)Hand.MyInstance.MyMoveable;
                    AddItem(armor);
                    EquipScreen.MyInstance.MySelectedButton.DequipArmor();
                    Inventory.MyInstance.AddItem(Hand.MyInstance.MyMoveable as Item);
                    (Hand.MyInstance.MyMoveable as Item).Slot = this;
                    Hand.MyInstance.Drop();
                }
                if(Hand.MyInstance.MyMoveable is WeaponItem)
                {
                    WeaponItem weapon = (WeaponItem)Hand.MyInstance.MyMoveable;
                    AddItem(weapon);
                    EquipScreen.MyInstance.MySelectedButton.DequipWeapon();
                    Inventory.MyInstance.AddItem(Hand.MyInstance.MyMoveable as Item);
                    (Hand.MyInstance.MyMoveable as Item).Slot = this;
                    Hand.MyInstance.Drop(); 
                }
            }
            else if(Inventory.MyInstance.FromSlot != null)//if we have something to move
            {
                //If the inventory has something in its fromslot, which means theres an item on the move
                Debug.Log("There is something in the FromSlot");
                if(PutItemBack() || MergeItems(Inventory.MyInstance.FromSlot) ||SwapItems(Inventory.MyInstance.FromSlot)||AddItems(Inventory.MyInstance.FromSlot.itemStack))
                {
                    Hand.MyInstance.Drop();
                    Inventory.MyInstance.FromSlot = null;
                    Inventory.MyInstance.equipInventory.FromSlot = null;
                }
            }
            
        }
        if(eventData.button == PointerEventData.InputButton.Right && Hand.MyInstance.MyMoveable == null)
        {
            UseItem();
        }
    }
    public void UseItem()
    {
        if(MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
        else if(MyItem is ArmorItem)
        {
            (MyItem as ArmorItem).Equip();
        }
        else if (MyItem is WeaponItem)
        {
            (MyItem as WeaponItem).Equip();
        }
    }
    public bool StackItem(Item item)
    {
       // Debug.Log("Code has reached StackItem");
       // Debug.Log("IsEmpty is " + IsEmpty + " and the item is " + item.name);
        if(MyItem != null)
        {
            if (!IsEmpty 
                && 
                item.name == MyItem.name /* MyItem.name is the name of the item on this slot */
                && 
                itemStack.Count < MyItem.StackSize)
                //Without the MyItem bullshit I would be able to stack a sword on a twilit orb
            {
                Debug.Log("Code is in the if statement of StackItem");
                itemStack.Push(item); //This pushes the item into the itemStack
                item.Slot = this;
                return true;
            }
        }
    //    Debug.Log("I was unable to stack the " + item.name);
        return false;
    }
    public bool PutItemBack()
    {
        if(Inventory.MyInstance.FromSlot == this) //if this is true then im trying to put it back
        {
            Debug.Log("Putting item back!");
            Inventory.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }

    private bool SwapItems(InventorySlots from)
    {
        if(IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.StackSize)
        {
            //Copy all items from A
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.itemStack);
            Debug.Log("SLOT A COPIED TO B");
            //Clear slot A
            from.itemStack.Clear();
            Debug.Log("SLOT A CLEARED");

            //Copy from B and put in A
            from.AddItems(itemStack);

            //Clear B
            itemStack.Clear();

            //Move from the copy of A to B
            AddItems(tmpFrom);
            return true;
        }
        return false;
    }

    private bool MergeItems(InventorySlots from)
    {
        /*
         *MergeItems will be checked only by the pointer 
         */
        Debug.Log("MergeItems have been called");
        if(IsEmpty)
        {
            return false;
        }
        if(from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            //How many free slots are there
            int freeSpace = MyItem.StackSize - MyCount;

            for(int i = 0; i < freeSpace; i++)
            {
                AddItem(from.itemStack.Pop());
            }
            return true;
        }
        return false;
    }

    private void UpdateSlot()
    {
        Debug.Log("UpdateSlot has been reached");
        if(UIManager.MyInstance == null)
        {
            Debug.Log("UIManager returns null");
        }
        UIManager.MyInstance.UpdateStackSize(this);
    }
}
