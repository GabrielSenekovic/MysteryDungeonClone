using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipModel : MonoBehaviour {

    public Transform WeaponParent;
    public Transform ShieldParent;
    public Transform HatParent;
    public Transform ShirtParent;
    public Transform PantsParent;
    public Transform ShoesParent;
    public Transform GlovesParent;
    public Transform CapeParent;
    public Transform WingParent;

    public ItemType m_EquippedWeapon = ItemType.None;
    private WeaponType m_EquippedWeaponType = WeaponType.None;
    private ItemType m_EquippedShield = ItemType.None;
    private ItemType m_EquippedPants = ItemType.None;
    [SerializeField]
    private EquipScreen m_EquipScreen;

    public void Equip(ItemType itemType, ItemData.EquipPosition equipPosition)
    {
        Debug.Log("EquipButton has called the Equip function");
        ItemData itemData = Database.Item.FindItem(itemType);
        if (equipPosition == ItemData.EquipPosition.SwordHand)
        {
            EquipWeapon(itemType);
        }
        else if (equipPosition == ItemData.EquipPosition.ShieldHand)
        {
            EquipShield(itemType);
        }
        else if (equipPosition == ItemData.EquipPosition.Hat)
        {
            EquipHat(itemType);
        }
        else if (equipPosition == ItemData.EquipPosition.Shirt)
        {
            EquipShirt(itemType);
        }
        else if (equipPosition == ItemData.EquipPosition.Pants)
        {
            EquipPants(itemType);
        }
    }

    public void EquipWeapon(ItemType itemType)
    {
        Debug.Log("Equipping weapon");
        EquipItem(itemType, ItemData.EquipPosition.SwordHand, WeaponParent, ref m_EquippedWeapon);
        /*m_EquippedWeapon = itemType;
        GameObject NewWeaponObject = (GameObject)Instantiate(itemData.Prefab); //So instantiate creates a child gameobject based on the swordprefab
        NewWeaponObject.transform.parent = WeaponParent; //THIS PUTS THE SWORDPREFAB AS A CHILD OF THE WEAPONPARENT OH MY G O D
        NewWeaponObject.transform.localPosition = Vector2.zero; //This makes sure that the new object is at x 0 and y 0.
        NewWeaponObject.transform.rotation = Quaternion.identity; //This apparently makes the object not rotate*/
    }
    public void EquipShield(ItemType itemType)
    {
        EquipItem(itemType, ItemData.EquipPosition.ShieldHand, ShieldParent, ref m_EquippedShield);
    }
    public void EquipHat(ItemType itemType)
    {
        return;
    }
    public void EquipShirt(ItemType itemType)
    {
        return;
    }
    public void EquipPants(ItemType itemType)
    {
        EquipItem(itemType, ItemData.EquipPosition.Pants, PantsParent, ref m_EquippedPants);
    }
    void EquipItem(ItemType itemType, ItemData.EquipPosition equipPosition, Transform itemParent, ref ItemType equippedItemSlot)
    {
        if (itemParent == null)
        {
            return;
        }
        ItemData itemData = Database.Item.FindItem(itemType);

        if (itemData == null)
        {
            return;
        }
        if (itemData.EquipableHand != equipPosition)
        {
            return;
        }
        equippedItemSlot = itemType;
        GameObject newItemObject = (GameObject)Instantiate(itemData.Prefab);
        newItemObject.transform.parent = itemParent;
        newItemObject.transform.localPosition = Vector2.zero;
        newItemObject.transform.rotation = Quaternion.identity;
        m_EquippedWeaponType = itemData.WeaponType;
        Debug.Log("Equipped weapon is a " + GetEquippedWeaponType());
    }
    public ItemType GetEquippedShield()
    {
        return m_EquippedShield;
    }


    public ItemType GetEquippedWeapon()
    {
        return m_EquippedWeapon;
    }

    public WeaponType GetEquippedWeaponType()
    {
        return m_EquippedWeaponType;
    }

}
