using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ItemData.EquipPosition m_equipPosition;

    private ArmorItem equippedArmorItem;
    private WeaponItem equippedWeaponItem;

    [SerializeField]
    private Image icon;

    private Inventory m_Inventory;

    [SerializeField]
    private CharacterEquipModel m_EquipModel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Hand.MyInstance.MyMoveable is ArmorItem)
            {
                Debug.Log("Now equipping: " + Hand.MyInstance.MyMoveable);
                ArmorItem tmp = (ArmorItem)Hand.MyInstance.MyMoveable;
                if (tmp.MyEquipPosition == m_equipPosition)
                {
                    Debug.Log(tmp.MyEquipPosition + " and " + m_equipPosition);
                    m_EquipModel.Equip(tmp.itemType, m_equipPosition); // equips item in the correct position 
                    EquipArmor(tmp);
                }
            }
            else if (Hand.MyInstance.MyMoveable is WeaponItem)
            {
                Debug.Log("Now equipping: " + Hand.MyInstance.MyMoveable);
                WeaponItem tmp = (WeaponItem)Hand.MyInstance.MyMoveable;
                if (tmp.MyEquipPosition == m_equipPosition)
                {
                    m_EquipModel.Equip(tmp.itemType, m_equipPosition); // equips item in the correct position 
                    EquipWeapon(tmp);
                }
            }
            else if (Hand.MyInstance.MyMoveable == null)
            {//checks if hand is empty and if the slot we click on isnt empty
                //Basically here is where we dequip shit
                if (equippedArmorItem != null)
                {
                    Hand.MyInstance.TakeMoveable(equippedArmorItem);
                    EquipScreen.MyInstance.MySelectedButton = this;
                    icon.color = Color.grey;
                    Debug.Log("Data of item we dequip: MyMoveable: " + Hand.MyInstance.MyMoveable +
                        " Name of the equipped weapon: " + equippedArmorItem.name);
                }
                else if (equippedWeaponItem != null)
                {
                    Hand.MyInstance.TakeMoveable(equippedWeaponItem);
                    EquipScreen.MyInstance.MySelectedButton = this;
                    icon.color = Color.grey;
                    Debug.Log("Data of item we dequip: MyMoveable: " + Hand.MyInstance.MyMoveable + 
                        " Name of the equipped weapon: " + equippedWeaponItem.name);
                }
            }
        }
    }
    public void EquipArmor(ArmorItem armorInHand)
    {
        Debug.Log("EquipArmor on the button has been reached.");
        //The armor in this case is tmp from the OnPointerClick function
        //The tmp is the Hand.MyInstance.MyMoveable
        //Which means it is the weapon that the hand is holding
        //Call this function only if the item is picked up from the equipinventory and not the equipbutton
        armorInHand.Remove(); //This is the part of the code that removes the item from its inventory slot
        if (equippedArmorItem != null)
        {
            //This checks if there is an armor equipped already
            if (equippedArmorItem != armorInHand)
            {
                Debug.Log("The equipped armor is not the same as the one the hand holds");
                Inventory.MyInstance.FindItemInInventoryAndRemove(armorInHand as Item);
                if (EquipScreen.MyInstance.GetComponentInChildren<EquipInventory>().FromSlot != null)
                {
                    //If the fromslot is not null, that means the item was most certainly picked up from the inventory
                    //It checks this, because you could have picked the item up and then placed it back from the equipbutton
                    //No but wait that would be impossible if the items arent the same item tho
                    Inventory.MyInstance.AddItem(equippedArmorItem as Item);
                    (Hand.MyInstance.MyMoveable as Item).Slot = EquipScreen.MyInstance.GetComponentInChildren<EquipInventory>().FromSlot;
                }
                Debug.Log("I have: " + equippedArmorItem + " equipped and a: " + armorInHand + "In my hand.");
                armorInHand.Slot.AddItem(equippedArmorItem); //Swaps the slot you picked up your armor from to the armor in the equipbutton
                Debug.Log(armorInHand.Slot.MyItem);
            }
            if(equippedArmorItem == armorInHand)
            {
                Debug.Log("The equipped armor and the armor in the hand are the same");
                armorInHand.Slot.AddItem(equippedArmorItem);
            }
        }
        else
        {
            Inventory.MyInstance.FindItemInInventoryAndRemove(armorInHand as Item);
        }

        icon.enabled = true;
        icon.sprite = armorInHand.MyIcon;
        icon.color = Color.white;
        equippedArmorItem = armorInHand; // a reference to the equipped armor

        if (Hand.MyInstance.MyMoveable == (armorInHand as IMoveable)) //this makes sure we can equip an armor while we hold a potion in our hand
        {
            Debug.Log(Hand.MyInstance.MyMoveable + " and " + armorInHand);
            Hand.MyInstance.Drop();
        }
        Debug.Log("Code has finished equipping the weapon on the button");

    }
    public void EquipWeapon(WeaponItem weaponInHand)
    {
        Debug.Log("EquipWeapon on the button has been reached.");
        //The weapon in this case is tmp from the OnPointerClick function
        //The tmp is the Hand.MyInstance.MyMoveable
        //Which means it is the weapon that the hand is holding
        //Call this function only if the item is picked up from the equipinventory and not the equipbutton
        weaponInHand.Remove(); //This weapon is the weapon in the hand and here it removes itself from its slot. If its not here, the weapons will keep stacking
        if (equippedWeaponItem != null)
        {
            if(equippedWeaponItem != weaponInHand)
            {
                Inventory.MyInstance.FindItemInInventoryAndRemove(weaponInHand as Item);
                if (EquipScreen.MyInstance.GetComponentInChildren<EquipInventory>().FromSlot != null)
                {
                    Inventory.MyInstance.AddItem(equippedWeaponItem as Item);
                    (Hand.MyInstance.MyMoveable as Item).Slot = EquipScreen.MyInstance.GetComponentInChildren<EquipInventory>().FromSlot;
                }
                Debug.Log("I have: " + equippedWeaponItem + " equipped and a: " + weaponInHand + "In my hand.");
                //If the weapon in the equipbutton is not the weapon on the hand
                //For example if the hand is holding a bow and I have a sword equipped
                weaponInHand.Slot.AddItem(equippedWeaponItem);
                Debug.Log(weaponInHand.Slot.MyItem);
            }
            if (equippedWeaponItem == weaponInHand)
            {
                weaponInHand.Slot.AddItem(equippedWeaponItem);
            }
        }
        else
        {
            Inventory.MyInstance.FindItemInInventoryAndRemove(weaponInHand as Item);
        }
        icon.enabled = true;
        icon.sprite = weaponInHand.MyIcon;
        icon.color = Color.white;
        equippedWeaponItem = weaponInHand;

        if (Hand.MyInstance.MyMoveable == (weaponInHand as IMoveable))
        {
            //This part makes sure the item on the hand is removed
            Debug.Log(Hand.MyInstance.MyMoveable + " and " + weaponInHand);
            Hand.MyInstance.Drop();
        }
        Debug.Log("Code has finished equipping the weapon on the button");
    }
    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;
        equippedArmorItem = null;
    }
    public void DequipWeapon()
    {
        icon.color = Color.white;
        icon.enabled = false;
        equippedWeaponItem = null;
    }
}
