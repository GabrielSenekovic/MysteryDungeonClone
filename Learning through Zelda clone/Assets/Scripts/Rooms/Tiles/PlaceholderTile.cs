using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class PlaceholderTile : Tile
{
    [SerializeField]
    public Tile[] placeholderWallTiles; //this is the amount of tiles in this tileset. These tiles each have a number, which will be replaced by another set of tiles

    [SerializeField]
    public Tile[] placeholderFloorTiles; //this is the amount of tiles in this tileset. These tiles each have a number, which will be replaced by another set of tiles

    //The first tilemap script Ive ever attempted to code

    //I want to be able to switch the tilemap to another tilemap I have

#if UNITY_EDITOR //this code will not be excecuted if we export the game

    [MenuItem("Assets/Create/Tiles/PlaceholderTile")]
    public static void CreatePlaceholderTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save PlaceholderTile", "New PlaceholderTile", "asset", "Save PlaceholderTile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PlaceholderTile>(), path);
    }

#endif
}
