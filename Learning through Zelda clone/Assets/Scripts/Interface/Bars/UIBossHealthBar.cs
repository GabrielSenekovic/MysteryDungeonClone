using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealthBar : MonoBehaviour {

    public AttackableEnemy HealthModel;
    // public RectTransform HealthBar; //This is what you use if you want to change the size of the healthbar
    //Thats not what we're gonna do
    private static UIBossHealthBar instance;
    private Image HealthBar;
    private Image HealthBarBackground;
    public Text HealthText;
    private float currentFill;
    public float BarChangeSpeed;

    public static UIBossHealthBar MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIBossHealthBar>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        HealthBar = GetComponentInChildren<Image>();
        HealthBarBackground = transform.parent.GetComponentInChildren<Image>();
        if (HealthModel == null)
        {
            HealthBar.enabled = false;
            HealthBarBackground.enabled = false;
        }
    }

    private void Update()
    {
        if(HealthModel != null)
        {
            UpdateText();
            UpdateHealthBar();
        }
    }
    void UpdateText()
    {
        if (HealthText != null)
        {
            HealthText.text = Mathf.RoundToInt(HealthModel.GetHealth()) + "/" + Mathf.RoundToInt(HealthModel.GetMaximumHealth());
        }

    }
    private void UpdateHealthBar()
    {
        currentFill = HealthModel.GetHealthPercentage();
        if (currentFill != HealthBar.fillAmount)
        {
            Debug.Log("Fillamount is: " + HealthBar.fillAmount + " and  healthpercentage is: " + HealthModel.GetHealthPercentage());
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, currentFill, Time.deltaTime * BarChangeSpeed);
        }
    }
    public void RevealImage()
    {
        HealthBar.enabled = true;
        HealthBarBackground.enabled = true;
    }
}
