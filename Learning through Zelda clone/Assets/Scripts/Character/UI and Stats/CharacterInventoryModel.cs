using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventoryModel : MonoBehaviour
{

    private CharacterAdvancedMovementModel m_MovementModel;
    private CharacterEquipModel m_EquipModel;
    public Inventory inventory;

    public void Awake()
    {
        m_MovementModel = GetComponent<CharacterAdvancedMovementModel>();
        m_EquipModel = GetComponent<CharacterEquipModel>();
    }

    public int GetItemCount(ItemType itemType)
    {
        if (inventory.m_Items.ContainsKey(itemType) == false)
        {
            return 0;
        }
        return inventory.m_Items[itemType];
    }

    public void AddItem(ItemType itemType, PickupType pickupType)
    {
        AddItem(itemType, 1, pickupType);
    }
    public void AddItem(ItemType itemType, int amount, PickupType pickupType)
    {
        Debug.Log("Code has reached AddItem, adding: " + itemType);
        if (amount > 0)
        {
            ItemData itemData = Database.Item.FindItem(itemType);
            Debug.Log(itemType + " is a " + itemData.name);
            if (itemData != null)
            {
                if (itemData.Animation != ItemData.PickupAnimation.None)
                {
                    m_MovementModel.ShowItemPickup(itemType, pickupType);
                }
                if(itemData.EquipOnPickup == true)
                {
                    Debug.Log("Enter the equipping zone with " + itemData.EquipableHand);
                    if (itemData.EquipableHand == ItemData.EquipPosition.SwordHand)
                    {
                        m_EquipModel.EquipWeapon(itemType);
                    }
                    else if (itemData.EquipableHand == ItemData.EquipPosition.ShieldHand)
                    {
                        m_EquipModel.EquipShield(itemType);
                    }
                    WeaponItem weapon = itemData.Item as WeaponItem;
                    EquipScreen.MyInstance.EquipWeapon(weapon);
                }
            }
            if(itemData.EquipOnPickup != true)
            {
                for (int i = 0; i < amount; i++) //this SHOULD call additem once for every amount you pick up
                {
                    Debug.Log("Inventory For loop reached");
                    if (itemType != ItemType.Money)
                    {
                        inventory.FindItemBeforeAdd(itemType);
                    }
                    if (itemType == ItemType.Money)
                    {
                        if (inventory.m_Items.ContainsKey(itemType) == true)
                        {
                            inventory.m_Items[itemType] += amount; //This will add one of the item as declared by the enum
                            Debug.Log("Adding money");
                        }
                        else
                        {
                            inventory.m_Items.Add(itemType, amount); //This will add the enum item to the dictionary if its not already there
                            Debug.Log("Adding money");
                            break;
                        }
                    }
                }
            }

            Debug.Log(amount + " " + itemType + " added");
        }
    }
}
