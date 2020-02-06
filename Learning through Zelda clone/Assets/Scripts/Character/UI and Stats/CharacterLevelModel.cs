using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevelModel : MonoBehaviour {
    public int level;
    public int exp;
    public int totalExp; // total exp is never depleted, while exp turns to 0 when the level is changed
    public int maxExp; //changes every level to something else. This is the ammount the exp must get to in order to level up
    public bool levelUpdating;
    private int expRest;

    public void Awake()
    {
        levelUpdating = false;
        if(level - 1 != 0)
        {
            maxExp = 100 * level * (level - 1);
        }
        else
        {
            maxExp = 100;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddEXP(10);
        }
        if (exp >= maxExp & levelUpdating != true)
        {
            LevelUp();
        }
    }

    public float GetEXPAmount()
    {
        return exp;
    }
    public int GetTotalEXPAmount()
    {
        return totalExp;
    }
    public int GetLevel()
    {
        return level;
    }
    public int GetMaxEXP()
    {
        return maxExp;
    }
    public float GetEXPPercentage()
    {
        return (float)exp / maxExp;
    }
    public void LevelUp()
    {
        expRest = exp - maxExp;
        level ++;
        levelUpdating = true;
    }
    public void SetNewEXPAmount()
    {
        exp = 0;
        if (expRest != 0)
        {
            exp = expRest;
            expRest = 0;
        }
    }
    public void AddEXP(int expToAdd)
    {
        exp = exp + expToAdd;
        totalExp = totalExp + expToAdd;
    }

    public void SetNewEXPLimit()
    {
        Debug.Log("Level is: " + level);
        maxExp = 100 * level * (level - 1);
        Debug.Log("new exp target is: " + maxExp);
    }
}
