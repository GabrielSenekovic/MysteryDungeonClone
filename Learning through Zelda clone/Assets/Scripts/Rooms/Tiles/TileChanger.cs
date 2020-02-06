using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class TileChanger : MonoBehaviour {

    [SerializeField]
    private Tile[] replacementWallTiles;
    [SerializeField]
    private Tile[] WallTiles; //this will be the tiles that will change their sprites

    [SerializeField]
    private Tile[] replacementFloorTiles;
    [SerializeField]
    private Tile[] FloorTiles;

    public WallTileMap wallTileMap;
    public FloorTileMap floorTileMap;

    public bool isWall;

    public RoomManager roomManager;


    public void Awake()
    {
        roomManager = RoomManager.MyInstance;
    }

    public bool CheckIfDebug()
    {
     //   Debug.Log("The ammount of floor tile maps is " + ammountOfFloorTileMaps);
        if (wallTileMap == WallTileMap.Debug && floorTileMap == FloorTileMap.Debug)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReplaceFlooring(int tilemapIdentifier)
    {
        FloorTileMap findThisFloorTileMap = (FloorTileMap)tilemapIdentifier;
        roomManager.GetComponent<TileReplacementManager>().GetFloorTileMap(findThisFloorTileMap);
        //Now the instance should have the appropriate tiles
        replacementFloorTiles = roomManager.GetComponent<TileReplacementManager>().replacementFloorTileMapsArray;
            //Now this code should have the appropriate tiles
            for (int entryNumber = 0; entryNumber < replacementFloorTiles.Length; entryNumber++) //I made a foreach loop here before to try to do the same thing and got an "array out of range" error
            {
              //  Debug.Log("Loop number: " + entryNumber);
                //FloorTiles[entryNumber].sprite = replacementFloorTiles[entryNumber];
                GetComponent<Tilemap>().SwapTile(FloorTiles[entryNumber], replacementFloorTiles[entryNumber]);
                // replacementFloorTiles.CopyTo(FloorTiles, 0);
               // GetComponent<Tilemap>().RefreshAllTiles();
            }
       
    }

    public void ChangeFlooringColor(float red, float green, float blue)
    {
        foreach (Tile tile in replacementFloorTiles)
        {
            tile.color = new Color(red, green, blue);
            GetComponent<Tilemap>().RefreshAllTiles();
        }
    }

    public void ReplaceWalls(int tilemapIdentifier)
    {
       // Debug.Log("ReplaceWalls has been reached");
     //   Debug.Log("The random number for walls is " + num);
        WallTileMap findThisWallTileMap = (WallTileMap)tilemapIdentifier;
        roomManager.GetComponent<TileReplacementManager>().GetWallTileMap(findThisWallTileMap, gameObject);
        //Now the instance should have the appropriate tiles
        replacementWallTiles = roomManager.GetComponent<TileReplacementManager>().replacementWallTileMapsArray;
        //Now this code should have the appropriate tiles
        for (int entryNumber = 0; entryNumber < replacementWallTiles.Length; entryNumber++) //I made a foreach loop here before to try to do the same thing and got an "array out of range" error
        {
          //  Debug.Log("Loop number: " + entryNumber);
            GetComponent<Tilemap>().SwapTile(WallTiles[entryNumber], replacementWallTiles[entryNumber]);
            GetComponent<Tilemap>().RefreshAllTiles();
        }
    }

    public void ChangeWallColor(float red, float green, float blue)
    {
        foreach (Tile tile in replacementWallTiles)
        {
            Debug.Log("we got " + red + "," + green + ", " + blue);
            if(tile == null)
            {
                Debug.Log("This tile was null!");
            }
            tile.color = new Color(red, green, blue);
            GetComponent<Tilemap>().RefreshAllTiles();
        }
    }

    public void SetObligatoryWallTiles(WallTilemapIdentifier identifierWithOblitaryWalls)
    {
        Debug.Log("SetObligatoryWallTiles has been reached");
        replacementWallTiles = identifierWithOblitaryWalls.obligatoryTilemap.placeholderWallTiles;
        int[] arrayIndexForObligatoryTiles = identifierWithOblitaryWalls.arrayIndexForObligatoryTilemap;

        for (int entryNumber = 0; entryNumber < arrayIndexForObligatoryTiles.Length; entryNumber++) 
            //this loop has to take each entry in the obligatory tilemap and replace the appropriate tile in this tilemap using the array index
        {
           // Debug.Log("Loop number: " + entryNumber);
            //FloorTiles[entryNumber].sprite = replacementFloorTiles[entryNumber];
            int indexOfTileToReplace = arrayIndexForObligatoryTiles[entryNumber];
            Debug.Log("Index of tile to replace is: " + arrayIndexForObligatoryTiles[entryNumber]);
            GetComponent<Tilemap>().SwapTile(WallTiles[indexOfTileToReplace], identifierWithOblitaryWalls.obligatoryTilemap.placeholderWallTiles[entryNumber]);
            // replacementFloorTiles.CopyTo(FloorTiles, 0);
            Debug.Log("Time to refresh colors");
            RoomManager.MyInstance.GetComponent<TileReplacementManager>().GetRandomColorForObligatoryWall(gameObject);
        }

        foreach (var position in GetComponent<Tilemap>().cellBounds.allPositionsWithin)
        {
            if (GetComponent<Tilemap>().HasTile(position))
            {
                TileBase tile = GetComponent<Tilemap>().GetTile(position);
                if (WallTiles.Contains(tile))
                {
                    GetComponent<Tilemap>().SetTile(position, null);
                }
            }
        }
    }
}
