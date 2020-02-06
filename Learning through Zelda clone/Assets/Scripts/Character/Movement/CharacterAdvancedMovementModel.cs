using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAdvancedMovementModel : CharacterMovementModel {

    public Transform PreviewItemParent;
    private ItemType m_PickingUpObject = ItemType.None;
    private PickupType m_PickingUpObjectType = PickupType.None;

    public BoxCollider2D physicalAttack;
    public BoxCollider2D vine;

    private GameObject m_PickupItem;
    private void OnDrawGizmos() //Oliver explains that gizmos helps you create scripts that can only be seen in the editor
    {
        Gizmos.color = Color.red; //shows room outline. Always the size of the room
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0));
    }
    override public void UpdateDirection()
    {
        if (IsFrozen())
        {
            if (m_ReceivedDirection != Vector2.zero && GetItemThatIsBeingPickedUp() != ItemType.None && GetTimeSinceFrozen() > 0.5f)
            {
                m_PickingUpObject = ItemType.None;
                SetFrozen(false, false, true);
                Destroy(m_PickupItem);
            }
        }
        if (m_IsDirectionFrozen == true && m_ReceivedDirection != Vector2.zero)
        {
            return;
        }

        if (m_IsAttacking == true)
        {
            return;
        }

        if (IsBeingPushed() == true)
        {
            m_MovementDirection = m_PushDirection;
            return;
        }

        if (Time.frameCount == m_LastSetDirectionFrameCount)
        {
            return;
        }

        m_MovementDirection = new Vector3(m_ReceivedDirection.x, m_ReceivedDirection.y, 0);

        if (m_ReceivedDirection != Vector2.zero)
        {
            Vector3 facingDirection = m_MovementDirection;
            if(facingDirection.x != 0)
            {
                facingDirection.y = 0;
            }
            else
            {
                facingDirection.x = 0;
            }
            m_FacingDirection = facingDirection;
            if (gameObject.tag == "Player")
            m_LastSetDirectionFrameCount = Time.frameCount;
        }
    }
    public void ShowItemPickup(ItemType itemType, PickupType pickupType)
    {
        if (PreviewItemParent == null)
        {
            return;
        }
        ItemData itemData = Database.Item.FindItem(itemType);
        if (itemData == null)
        {
            return;
        }
        SetDirection(new Vector2(0, -1));
        SetFrozen(true, true, true);

        m_PickingUpObject = itemType;
        m_PickingUpObjectType = pickupType;

        m_PickupItem = (GameObject)Instantiate(itemData.Prefab);

        m_PickupItem.transform.parent = PreviewItemParent;
        m_PickupItem.transform.localPosition = Vector2.zero;
        m_PickupItem.transform.localRotation = Quaternion.identity;

        MonoBehaviour[] components = m_PickupItem.GetComponentsInChildren<MonoBehaviour>();

        foreach (MonoBehaviour component in components)
        {
            component.enabled = false;
            //This disables literally every component in the item
        }
    }
    public ItemType GetItemThatIsBeingPickedUp()
    {
        return m_PickingUpObject;
    }
    public PickupType GetPickUpType()
    {
        return m_PickingUpObjectType;
    }

}
