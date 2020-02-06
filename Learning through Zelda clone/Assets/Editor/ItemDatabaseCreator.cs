using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabaseCreator : MonoBehaviour {
    //Fucking finally found this motherfucker
    [MenuItem("Custom Menu/Databases/Create Item Database")]//This adds the Let's Code Games bar to the menu bar. Clicking Create Item Database excecutes the CreateItemDatabase Function below
    public static void CreateItemDatabase()
    {
        ItemDatabase newDatabase = ScriptableObject.CreateInstance<ItemDatabase>();
        AssetDatabase.CreateAsset(newDatabase, "Assets/ItemDatabase.asset");
        AssetDatabase.Refresh();
        //AssetDatabase can only be accessed with using UnityEditor
    }
}
