using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public struct TileMap
{
    public GameObject prefab;
    public GameObject parent;

    // Tile Vars
    public int rows;    // Number of rows will have the HeatMap
    public int columns; // Number of Columns will have the HeatMap
    public int width;   // Width of the Map that want to be covered
    public int height;  // Height of the Map that want to be covered
    
    public List<IDatabaseEntity> skibidi;
}