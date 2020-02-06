using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{
    private static Hand instance;
    public static Hand MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Hand>();
            }
            return instance;
        }
    }


    public IMoveable MyMoveable {get; set;}

    private Image icon;

    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        icon = GetComponent<Image>();
    }

    private void Update()
    {
        icon.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        icon.transform.position = new Vector3(transform.position.x, transform.position.y, 20f);
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }
    }

    public void TakeMoveable(IMoveable moveable)
    {
        Debug.Log("Taking moveable");
        MyMoveable = moveable;
        Debug.Log("The hand is holding a: " + MyMoveable);
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        Inventory.MyInstance.FromSlot = null;
        Inventory.MyInstance.equipInventory.FromSlot = null;
    }

    public void DeleteItem()
    {
        if (MyMoveable is Item && Inventory.MyInstance.FromSlot != null)
        {
            (MyMoveable as Item).Slot.Clear();
        }
        Drop();
        Inventory.MyInstance.FromSlot = null;
        Inventory.MyInstance.equipInventory.FromSlot = null;
    }
    public Image GetIcon()
    {
        return icon;
    }
}
