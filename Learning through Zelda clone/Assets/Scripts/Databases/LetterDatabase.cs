using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterDatabase : ScriptableObject
{
    public List<LetterData> Data;

    public LetterData FindLetter(char letter)
    {
        for (int i = 0; i < Data.Count; ++i)
        {
            if (Data[i].letter == letter)
            {
                return Data[i];
            }
        }
        return null;
    }
}
[System.Serializable]
public class LetterData
{
    public char letter;
    public GameObject Prefab;
    public float nextPositionOffset; //This is where the next letter should spawn in relation to this letter. Its an X value
}
