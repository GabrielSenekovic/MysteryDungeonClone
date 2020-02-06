using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Armor", menuName = "Create new Icon/New Armor", order = 1)]

public class ArmorItem : Item {

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



    /*public override string GetDescription()
    {
        string stats = string.Empty;
        if(defence > 0)
        {
            stats += string.Format("\n +{0} defence", defence);
        }
        return base.GetDescription() + stats;
    }*/

    public void Equip()
    {
        EquipScreen.MyInstance.EquipArmor(this);
    }
}
