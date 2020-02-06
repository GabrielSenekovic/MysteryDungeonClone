using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : ScriptableObject
{
    public List<ItemData> Data;

    public ItemData FindItem(ItemType itemType)
    {
        for (int i = 0; i < Data.Count; ++i)
        {
            if(Data[i].Type == itemType)
            {
                return Data[i];
            }
        }
        return null;
    }
}
[System.Serializable]
public class ItemData
{
    public string name;
    public enum PickupAnimation
    {
        None,
        OneHanded,
        TwoHanded,
    }
    public enum EquipPosition
    {
        NotEquipable,
        SwordHand,
        ShieldHand,
        Hat,
        Shirt,
        Pants,
        Shoes,
        Gloves,
        Cape,
        Wings,
        Necklace,
    }
    public ItemType Type;
    public Item Item;
    public WeaponType WeaponType;
    public GameObject Prefab;
    public bool EquipOnPickup;
    public EquipPosition EquipableHand;
    public PickupAnimation Animation;
}