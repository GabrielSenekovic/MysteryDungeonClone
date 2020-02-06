using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour {
    public ItemType ItemType;
    public int Amount;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        CharacterInventoryModel inventoryModel = collider.GetComponent<CharacterInventoryModel>();
        if(inventoryModel != null)
        {
            inventoryModel.AddItem(ItemType, Amount, PickupType.FromDrop);
            Destroy(gameObject);
        }
    }

}
