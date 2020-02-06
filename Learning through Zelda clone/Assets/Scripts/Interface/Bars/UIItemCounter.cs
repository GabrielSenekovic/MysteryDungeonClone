using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemCounter : MonoBehaviour {
    public CharacterInventoryModel Inventory;
    public ItemType ItemType;
    public string NumberFormat;
    Text m_Text;
    private void Awake()
    {
        m_Text = GetComponent<Text>();
    }
    private void Update()
    {
        if(Inventory == null)
        {
            return;
        }
        m_Text.text = Inventory.GetItemCount(ItemType).ToString(NumberFormat); //Here the text gets to know what to display from the inventory
        //Debug.Log(m_Text.text);
    }
}
