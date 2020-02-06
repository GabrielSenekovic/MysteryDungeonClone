using UnityEngine;
using System.Collections;

public class InteractableChest : InteractableBase
{
    public Sprite OpenChestSprite;
    public ItemType ItemInChest;
    public int Amount;
    private bool m_IsOpen;
    public bool m_Destructible;
    private SpriteRenderer m_Renderer;

    void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        IsInteractable = true;
    }
     
    public override void OnInteract(Character character)
    {
        Debug.Log("Interact with Chest");
        if (m_IsOpen == true) //Here it makes it so that if the chest is open, it can no longer do anything
        {
            return;
        }
        Character.Instance.Inventory.AddItem(ItemInChest, Amount, PickupType.FromChest);
        m_Renderer.sprite = OpenChestSprite; //Here it does so that if this action is excecuted, the object switches to this sprite
        m_IsOpen = true;
        transform.gameObject.tag = "Untagged";
        if(m_Destructible == true)
        {
            Debug.Log("Destroying Chest");
            DestroyChest();
        }
        IsInteractable = false;
    }
    public void DestroyChest()
    {
        Destroy(gameObject);
    }
}