using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public CharacterHealthModel HealthModel;
   // public RectTransform HealthBar; //This is what you use if you want to change the size of the healthbar
    //Thats not what we're gonna do
    private Image HealthBar;
    public Text HealthText;
    private float currentFill;
    public float BarChangeSpeed;

    private void Start()
    {
        HealthBar = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        UpdateText();
        UpdateHealthBar();
    }
    void UpdateText()
    {
        if(HealthText != null)
        {
            HealthText.text = Mathf.RoundToInt(HealthModel.GetHealth()) + "/" + Mathf.RoundToInt(HealthModel.GetMaximumHealth());
        }
        
    }
    private void UpdateHealthBar()
    {
        currentFill = HealthModel.GetHealthPercentage();
        if (currentFill != HealthBar.fillAmount)
        {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, currentFill, Time.deltaTime * BarChangeSpeed);
        }
    }

}
