using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTilemapIdentifier : MonoBehaviour {

    //This script puts together the appropriate tilemap with its enum
    public PlaceholderTile thisTilemap;
    public PlaceholderTile obligatoryTilemap;
    public WallTileMap wallTileMap;

    public int[] arrayIndexForObligatoryTilemap;
}
