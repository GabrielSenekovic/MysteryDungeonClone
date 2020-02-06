using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReplacementManager : MonoBehaviour
{
    //This script will house all of the tilemaps listed under their enum. The placeholdertile script will then call upon this script, get the appropriate tilemap based on the enum
    //and place it in its replacementtilemap array
    [SerializeField]
    private WallTilemapIdentifier[] wallTileMapsArray;
    [SerializeField]
    public Tile[] replacementWallTileMapsArray;
    [SerializeField]
    private FloorTilemapIdentifier[] floorTileMapsArray;
    [SerializeField]
    public Tile[] replacementFloorTileMapsArray;

    [SerializeField]
    public TileChanger[] loadedTileMaps;

    public List<WallTileMap> changedWallTileMaps;
    public List<FloorTileMap> changedFloorTileMaps;

    public System.Random randomNumber = new System.Random(); //accessed for colors

    float obligatoryRed = 0;
    float obligatoryBlue = 0;
    float obligatoryGreen = 0;
    float floorRed = 0, floorGreen = 0, floorBlue = 0, wallRed = 0, wallGreen = 0, wallBlue = 0;

    [SerializeField]
    private int ammountOfWallTileMaps = WallTileMap.GetNames(typeof(WallTileMap)).Length; //This gets an array of all the things defined in the enum, then counts how many things are in the array
    //This seems to be the only way of counting the amount of things defined in an enum
    [SerializeField]
    private int ammountOfFloorTileMaps = FloorTileMap.GetNames(typeof(FloorTileMap)).Length;

    /* public void Update()
     {
         if (Input.GetKeyDown(KeyCode.F12))
         {
             ChangeFlooringColor();
         }
         if (Input.GetKeyDown(KeyCode.F11))
         {
             ChangeWallColor();
         }
     }*/

    public void UpdateAllTiles()
    {
        Debug.Log("Update all tiles has been called");
        System.Random r = new System.Random();
        int tilemapIdentifier = r.Next(1, ammountOfFloorTileMaps);
        Debug.Log("tilemapIdentifier is " + tilemapIdentifier);

        loadedTileMaps = FindObjectsOfType(typeof(TileChanger)) as TileChanger[];
        foreach (TileChanger tileChanger in loadedTileMaps)
        {
            if (tileChanger.CheckIfDebug()&&tileChanger.isWall!=true)
            {
                if(changedFloorTileMaps.Contains(tileChanger.floorTileMap))
                {
        //            Debug.Log("I already have this floor!");
                    //if you havent already changed this one tilemap
                    tileChanger.ReplaceFlooring(tilemapIdentifier);
                    tileChanger.ChangeFlooringColor(floorRed, floorGreen, floorBlue);
                }
                else
                {
                    //you have already changed this type of tilemap
               //     Debug.Log("This is a new type of floor!");
                    changedFloorTileMaps.Add(tileChanger.floorTileMap);
                    floorRed = GetRandomColors();
                    floorGreen = GetRandomColors();
                    floorBlue = GetRandomColors();
                    Debug.Log(floorRed + floorGreen + floorBlue + " for flooring");
                    tileChanger.ReplaceFlooring(tilemapIdentifier);
                    tileChanger.ChangeFlooringColor(floorRed, floorGreen, floorBlue);
                }
            }
            if (tileChanger.CheckIfDebug()&&tileChanger.isWall==true)
            {
                if (changedWallTileMaps.Contains(tileChanger.wallTileMap))
                {
                  //  if you havent already changed this one tilemap
                    tileChanger.ReplaceWalls(tilemapIdentifier);
                    tileChanger.ChangeWallColor(wallRed, wallGreen, wallBlue);
                }
                else
                {
                    Debug.Log("This is a new type of wall!");
                    changedWallTileMaps.Add(tileChanger.wallTileMap);
                    wallRed = GetRandomColors();
                    wallGreen = GetRandomColors();
                    wallBlue = GetRandomColors();
              Debug.Log(wallRed + ", " + wallGreen + ", "+ wallBlue + " for walls");
                    tileChanger.ReplaceWalls(tilemapIdentifier);
                    tileChanger.ChangeWallColor(wallRed, wallGreen, wallBlue);
                }
            }
        }
    }

    public float GetRandomColors()
    {
        float color = randomNumber.Next(0, 100);
        if (color >= 50)
        {
            color = 49;
        }
        color = color / 100;
        return color;
    }

    public void GetRandomColorForObligatoryWall(GameObject obligatoryTileMap)
    {
        if(obligatoryRed == 0)
        {
            obligatoryRed = GetRandomColors();
        }
        if(obligatoryGreen == 0)
        {
            obligatoryGreen = GetRandomColors();
        }
        if(obligatoryBlue == 0)
        {
            obligatoryBlue = GetRandomColors();
        }
        obligatoryTileMap.GetComponent<Tilemap>().color = new Color(obligatoryRed, obligatoryGreen, obligatoryBlue);
    }

    public void GetFloorTileMap (FloorTileMap randomisedFloorTileMap)
    {
        foreach (FloorTilemapIdentifier floorTileIdentifier in floorTileMapsArray)
        {
            if (floorTileIdentifier.floorTileMap == randomisedFloorTileMap)
            {
                Debug.Log("The appropriate tilemap for floor has been found!");
                replacementFloorTileMapsArray = floorTileIdentifier.thisTilemap.placeholderFloorTiles;
            }
        }
    }

    public void GetWallTileMap (WallTileMap randomisedWallTileMap, GameObject originalWallTileMap)
    {
        foreach(WallTilemapIdentifier wallTileIdentifier in wallTileMapsArray)
        {
            if(wallTileIdentifier.wallTileMap == randomisedWallTileMap)
            {
                Debug.Log("The appropriate tilemap for walls has been found! It is: " + wallTileIdentifier);
                replacementWallTileMapsArray = wallTileIdentifier.thisTilemap.placeholderWallTiles;
                if(wallTileIdentifier.obligatoryTilemap!= null)
                {
                    CreateObligatoryWallTileMap(wallTileIdentifier, originalWallTileMap);
                }
            }
        }
    }
    public void CreateObligatoryWallTileMap(WallTilemapIdentifier identifierWithOblitaryWalls, GameObject originalWallTileMap)
    {
        GameObject obligatoryTilemap = Instantiate(originalWallTileMap.gameObject, originalWallTileMap.gameObject.transform.position, transform.rotation);
        obligatoryTilemap.transform.parent = originalWallTileMap.transform.parent;
        obligatoryTilemap.GetComponent<TilemapRenderer>().sortingOrder = originalWallTileMap.GetComponent<TilemapRenderer>().sortingOrder - 1;
        obligatoryTilemap.GetComponent<TileChanger>().SetObligatoryWallTiles(identifierWithOblitaryWalls);
    }
}
