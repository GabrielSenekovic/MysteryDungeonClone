using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipScreen : MonoBehaviour
{

    private static EquipScreen instance;
    [SerializeField]
    public EquipInventory equipInventory;

    private CanvasGroup canvasGroup;

    [SerializeField]
    private EquipButton cape, gloves, hat, necklace, pants, shield, shirt, sword, shoes, wings;

    public EquipButton MySelectedButton { get; set; }

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    public static EquipScreen MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<EquipScreen>();
            }
            return instance;
        }
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
        equipInventory.DropItemUponClose();
    }

    public void EquipArmor(ArmorItem armor)
    {
        switch (armor.MyEquipPosition)
        {
            case ItemData.EquipPosition.Cape:
                cape.EquipArmor(armor);
                break;
            case ItemData.EquipPosition.Gloves:
                gloves.EquipArmor(armor);
                break;
            case ItemData.EquipPosition.Hat:
                hat.EquipArmor(armor);
                break;
            case ItemData.EquipPosition.Necklace:
                necklace.EquipArmor(armor);
                break;
            case ItemData.EquipPosition.Pants:
                pants.EquipArmor(armor);
                break;
            case ItemData.EquipPosition.Shirt:
                Debug.Log("Equipping a shirt");
                shirt.EquipArmor(armor);
                break;
            case ItemData.EquipPosition.Shoes:
                shoes.EquipArmor(armor);
                break;
            case ItemData.EquipPosition.Wings:
                wings.EquipArmor(armor);
                break;
        }
    }
    public void EquipWeapon(WeaponItem weapon)
    {
        Debug.Log("Weapon is being equipped");
        switch (weapon.MyEquipPosition)
        {
            case ItemData.EquipPosition.ShieldHand:
                shield.EquipWeapon(weapon);
                break;
            case ItemData.EquipPosition.SwordHand:
                sword.EquipWeapon(weapon);
                break;
        }

    }

}
