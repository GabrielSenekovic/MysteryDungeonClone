using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsOnDestroy : MonoBehaviour
{
    public ItemType DropItemType;
    public int Amount;

    [Range(0f,1f)]
    public float Probability;

   /* private void OnDestroy()
    {
        if (Application.isPlaying == true)
        /*
        For some reason, the code instantiated the objects in the editor, so that you got an error saying
        "some items were not cleaned up"
        Tutorial guy said "this is spooky", "this should not happen" (1:55:23 week 8)
        So here it checks to make sure to only call OnLootDrop when the game is on and not in the editor
        It didnt work anyway for some reason
        a ghost is living in Unity
        
        {
            OnLootDrop();
        }
    }*/

    void OnLootDrop()
    {
        float randomValue = Random.Range(0f, 1f);

        if(randomValue > Probability) //This is made so that if the probability is highest, it will always spawn
        {
            return;
        }

        ItemData data = Database.Item.FindItem(DropItemType);
        if (data == null || data.Prefab == null)
        {
            return;
        }
        Instantiate(data.Prefab, transform.position, Quaternion.identity);
        Debug.Log(DropItemType + " has spawned");
    }

}
