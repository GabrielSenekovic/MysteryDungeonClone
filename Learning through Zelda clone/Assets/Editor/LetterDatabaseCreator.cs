using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LetterDatabaseCreator : MonoBehaviour
{
    //Fucking finally found this motherfucker
    [MenuItem("Custom Menu/Databases/Create Letter Database")]//This adds the Let's Code Games bar to the menu bar. Clicking Create Item Database excecutes the CreateItemDatabase Function below
    public static void CreateLetterDatabase()
    {
        LetterDatabase newDatabase = ScriptableObject.CreateInstance<LetterDatabase>();
        AssetDatabase.CreateAsset(newDatabase, "Assets/LetterDatabase.asset");
        AssetDatabase.Refresh();
        //AssetDatabase can only be accessed with using UnityEditor
    }
}
