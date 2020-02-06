using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LayerSorter : MonoBehaviour
{

    private SortingGroup sortingGroup;
    SpriteRenderer rendererOfCollisionObject;
    float renderersYPosition;

    private List<TallObject> tallObjectList = new List<TallObject>();

    private void Awake()
    {
        sortingGroup = transform.parent.GetComponent<SortingGroup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tall Object")
        {
            TallObject tallObject = collision.transform.parent.GetComponent<TallObject>();
            tallObjectList.Add(tallObject);
            Debug.Log("The amount of TallObjects are: " + tallObjectList.Count);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Tall Object")
        {
            TallObject tallObject = collision.transform.parent.GetComponent<TallObject>();
            if (tallObjectList.Count == 0 || tallObject.MySpriteRenderer.sortingOrder - 1 < sortingGroup.sortingOrder)
            {
                //If there are no tallobjects in the list, or if the sorting order of the tree minus one is less than the players
                if (transform.position.y >= (-tallObject.MySpriteRenderer.bounds.extents.y) + tallObject.MySpriteRenderer.transform.position.y)
                {
                    sortingGroup.sortingOrder = tallObject.MySpriteRenderer.sortingOrder - 1;
                    tallObject.FadeOut();
                }
            }
            else if(tallObject.MySpriteRenderer.sortingOrder == sortingGroup.sortingOrder)
            {
                Debug.LogWarning("Player is on the same layer as the tall object!!");
            }
            else if (tallObject.MySpriteRenderer.sortingOrder > sortingGroup.sortingOrder)
            {
                Debug.Log("tree is in front of player");
                /*
                 *this checks if the tree is in front of the player
                 */
                if (transform.position.y <= (-tallObject.MySpriteRenderer.bounds.extents.y) + tallObject.MySpriteRenderer.transform.position.y)
                {
                  sortingGroup.sortingOrder = tallObject.MySpriteRenderer.sortingOrder + 1;
                  tallObject.FadeIn();
                }
                else
                {
                    tallObject.FadeOut();
                }
            }
            Debug.Log("The amount of TallObjects are: " + tallObjectList.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tall Object")
        {
            TallObject tallObject = collision.transform.parent.GetComponent<TallObject>();
            tallObject.FadeIn();
            tallObjectList.Remove(tallObject);
            Debug.Log("The amount of TallObjects are: " + tallObjectList.Count);
            if (tallObjectList.Count == 0)
            {
                sortingGroup.sortingOrder = 1;
            }
            else
            {
                tallObjectList.Sort();
                sortingGroup.sortingOrder = tallObjectList[0].MySpriteRenderer.sortingOrder - 1;
            }
        }
    }
}
