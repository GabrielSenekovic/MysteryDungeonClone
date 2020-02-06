using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEXPBar : MonoBehaviour {

    public CharacterLevelModel LevelModel;
    // public RectTransform HealthBar; //This is what you use if you want to change the size of the healthbar
    //Thats not what we're gonna do
    private Image EXPBar;
    public Text EXPText;
    private float currentFill;
    public float BarChangeSpeed;

    void Awake()
    {
        EXPBar = GetComponentInChildren<Image>();
        EXPBar.fillAmount = LevelModel.GetEXPPercentage();
    }

    void Update()
    {
        if (EXPBar.fillAmount >= 0.999f & LevelModel.levelUpdating == true)
        {
            Debug.Log("The fillamount has exceeded the previous percentage");
            //make sure this only runs when you are going from one level to another
            EXPBar.fillAmount = 0;
            LevelModel.SetNewEXPAmount();
            LevelModel.levelUpdating = false;
            LevelModel.SetNewEXPLimit();
        }
        UpdateText();
        UpdateEXPBar();
    }
    void UpdateText()
    {
        if (EXPText != null)
        {
            EXPText.text = Mathf.RoundToInt(LevelModel.GetEXPAmount()) + "/" + Mathf.RoundToInt(LevelModel.GetMaxEXP());
        }
    }
    void UpdateEXPBar()
    {
        currentFill = LevelModel.GetEXPPercentage();
        if (currentFill != EXPBar.fillAmount)
        {
            if(LevelModel.levelUpdating != true)
            {
                EXPBar.fillAmount = Mathf.Lerp(EXPBar.fillAmount, currentFill, Time.deltaTime * BarChangeSpeed);
            }
            else
            {
                EXPBar.fillAmount = Mathf.Lerp(EXPBar.fillAmount, 1, Time.deltaTime * BarChangeSpeed *2);
            }
        }
    }
}
