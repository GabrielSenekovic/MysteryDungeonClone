using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Inventory m_Inventory;

    [SerializeField]
    private EquipScreen m_EquipScreen;

    [SerializeField]
    private GameObject m_EXPBar;

    [SerializeField]
    private GameObject m_HPBar;

    [SerializeField]
    private GameObject m_MoneyCounter;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("You pressed F1");
            m_Inventory.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            m_EquipScreen.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            m_Inventory.CountEquipables();
        }
    }

    public void UpdateStackSize(IClickable clickable)
    {
        Debug.Log("UpdateStackSize has been reached");
        Debug.Log("MyCount is " +clickable.MyCount);
        if(clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            Debug.Log("Code has reached the UpdateStackSize ELSE section with " + clickable);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if(clickable.MyCount == 0) //Hides icon if slot is empty
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }
    public void RefreshTooltip()
    {

    }

    public void HideUI()
    {
        Image[] HPimages = m_HPBar.GetComponentsInChildren<Image>();
        m_HPBar.GetComponentInChildren<Text>().enabled = false;
        foreach(Image image in HPimages)
        {
            image.enabled = false;
        }
        Image[] EXPimages = m_EXPBar.GetComponentsInChildren<Image>();
        m_EXPBar.GetComponentInChildren<Text>().enabled = false;
        foreach (Image image in EXPimages)
        {
            image.enabled = false;
        }
        m_MoneyCounter.GetComponent<Image>().enabled = false;
        m_MoneyCounter.GetComponentInChildren<Text>().enabled = false;
    }
    public void RevealUI()
    {
        Image[] HPimages = m_HPBar.GetComponentsInChildren<Image>();
        m_HPBar.GetComponentInChildren<Text>().enabled = true;
        foreach (Image image in HPimages)
        {
            image.enabled = true;
        }
        Image[] EXPimages = m_EXPBar.GetComponentsInChildren<Image>();
        m_EXPBar.GetComponentInChildren<Text>().enabled = true;
        foreach (Image image in EXPimages)
        {
            image.enabled = true;
        }
        m_MoneyCounter.GetComponent<Image>().enabled = true;
        m_MoneyCounter.GetComponentInChildren<Text>().enabled = true;
    }
}
