using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAdvancedMovementView : CharacterMovementView
{
    private CharacterAdvancedMovementModel m_AdvancedMovementModel;

    void Start()
    {
        SetItemActive(m_EquipModel.WeaponParent, false);
    }
    public override void Update()
    {
      //  ShowShield(); this is for when you actually have a shield
        UpdateDirection();
      //  UpdatePickingUpAnimation();
        UpdateHurt();
      //  UpdateShield();
    }

    void UpdateShield()
    {
        Animator.SetBool("HasShield", m_EquipModel.GetEquippedShield() != ItemType.None);
    }

    void UpdatePickingUpAnimation()
    {
        bool isPickingUpTwoHanded = false;
        bool isPickingUpOneHanded = false;

        if (m_AdvancedMovementModel.IsFrozen() == true)
        {
            ItemType pickupItem = m_AdvancedMovementModel.GetItemThatIsBeingPickedUp();
            if (m_AdvancedMovementModel.GetItemThatIsBeingPickedUp() != ItemType.None)
            {
                ItemData itemData = Database.Item.FindItem(pickupItem);
                switch (itemData.Animation)
                {
                    case ItemData.PickupAnimation.OneHanded:
                        isPickingUpOneHanded = true;
                        break;
                    case ItemData.PickupAnimation.TwoHanded:
                        isPickingUpTwoHanded = true;
                        break;
                }
            }
        }
        Animator.SetBool("IsPickingUpTwoHanded", isPickingUpTwoHanded);
        Animator.SetBool("IsPickingUpOneHanded", isPickingUpOneHanded);
    }

    public void ShowWeapon()
    {
        SetItemActive(m_EquipModel.WeaponParent, true);
    }
    public void HideWeapon()
    {
        SetItemActive(m_EquipModel.WeaponParent, false);
    }
    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        SetSortingOrderOfItem(m_EquipModel.WeaponParent, sortingOrder);
    }
    public void SetSortingOrderOfPickupItem(int sortingOrder)
    {
        SetSortingOrderOfItem(m_AdvancedMovementModel.PreviewItemParent, sortingOrder);
    }
    public void ShowShield()
    {
        SetItemActive(m_EquipModel.ShieldParent, true);
    }
    public void HideShield()
    {
        SetItemActive(m_EquipModel.ShieldParent, false);
    }

    public void SetSortingOrderOfShield(int sortingOrder)
    {
        SetSortingOrderOfItem(m_EquipModel.ShieldParent, sortingOrder);
    }
    public void ForceShieldDirection(ShieldDirection direction)
    {
        //This is called by the CharacterAnimationListener.SetShieldDirection
        ShieldView shield = m_EquipModel.ShieldParent.GetComponentInChildren<ShieldView>();
        if (shield == null)
        {
            return;
        }
        shield.ForceShieldDirection(direction);
    }
    public void ReleaseShieldDirection()
    {
        ShieldView shield = m_EquipModel.ShieldParent.GetComponentInChildren<ShieldView>();
        if (shield == null)
        {
            return;
        }
        shield.ReleaseShieldDirection();
    }

}
