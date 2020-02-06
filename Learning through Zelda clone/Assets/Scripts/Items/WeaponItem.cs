using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create new Icon/New Weapon", order = 1)]
public class WeaponItem : Item {
    [SerializeField]
    private ItemData.EquipPosition m_equipPosition;

    [SerializeField]
    public ItemType itemType;

    internal ItemData.EquipPosition MyEquipPosition
    {
        get
        {
            return m_equipPosition;
        }
    }
    public void Equip()
    {
        Debug.Log(this);
        EquipScreen.MyInstance.EquipWeapon(this);
    }
}
