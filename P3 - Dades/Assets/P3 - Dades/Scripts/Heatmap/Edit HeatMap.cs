using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EditHeatMap : MonoBehaviour
{
    // Grid Vars
    private float f_Rows;
    private float f_Columns;

    // HeatMap Size Vars
    private float f_MapX;
    private float f_MapZ;

    // Tile Vars
    private float tileSize_X;
    private float tileSize_Z;
    private Vector3 tileSize;

    private float startX;
    private float startZ;

    // Manager Vars
    private GameObject parent;
    //public List<GameObject> tiles;
    public List<TileStruct> tiles;

    // Database Object
    [SerializeField] private DatabaseReceive dataRecieve;

    [SerializeField] private GameObject tempCube;

    public void GenerateTiles()
    {
        tiles = new List<TileStruct>();

        if (parent == null)
        {
            parent = TileMapManager.tileMap.parent;
        }

        DeleteTiles();
        CalculateTileSize();

        tileSize.x = (float)TileMapManager.tileMap.width / (float)TileMapManager.tileMap.columns;
        tileSize.z = (float)TileMapManager.tileMap.height / (float)TileMapManager.tileMap.rows;

        startX = -((float)TileMapManager.tileMap.columns - 1) * tileSize.x / 2f;
        startZ = ((float)TileMapManager.tileMap.rows - 1) * tileSize.z / 2f;

        for (int row = 0; row < TileMapManager.tileMap.rows; row++)
        {
            for (int column = 0; column < TileMapManager.tileMap.columns; column++)
            {
                Vector3 position = new Vector3(startX + column * tileSize.x, 0, startZ - row * tileSize.z) + transform.position;

                TileStruct tile = new TileStruct();
                tile.heat = 0;

                tile.tileGO = Instantiate(TileMapManager.tileMap.prefab, position, Quaternion.identity, transform);
                tile.tileGO.name = $"Slot ({row},{column})";

                // Cambiar el tamaño del tile
                tile.tileGO.transform.localScale = new Vector3(tileSize.x / tile.tileGO.transform.localScale.x, tile.tileGO.transform.localScale.y, tileSize.z / tile.tileGO.transform.localScale.z);

                Color baseColor = Color.white;
                Material randomMaterial = new Material(Shader.Find("Standard"));
                randomMaterial.color = baseColor;
                tile.tileGO.GetComponent<Renderer>().material = randomMaterial;

                tiles.Add(tile);
            }
        }
    }

    public void DeleteTiles()
    {
        if (parent != null)
        {
            for (int i = parent.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.transform.GetChild(i).gameObject);
            }

            tiles.Clear();
        }
    }

    private void CalculateTileSize()
    {
        Renderer renderer = TileMapManager.tileMap.prefab.GetComponent<Renderer>();
        if (renderer != null)
        {
            tileSize = renderer.bounds.size;
        }
        else
        {
            Collider collider = TileMapManager.tileMap.prefab.GetComponent<Collider>();
            if (collider != null)
            {
                tileSize = collider.bounds.size;
            }
            else
            {
                Debug.LogError("Tile Prefab must have a Renderer or Collider to calculate its size.");
                tileSize = Vector3.one;
            }
        }
    }

    public void ApplyFilters(FilterSettings filterSettings)
    {
        //Vaciamos la lista al inicio
        TileMapManager.tileMap.filteredList.Clear();

        if ((filterSettings.activeFilters & FilterType.Country) != 0)
        {
            
        }

        if ((filterSettings.activeFilters & FilterType.Gender) != 0)
        {
            
        }

        if ((filterSettings.activeFilters & FilterType.Age) != 0)
        {
            
        }

        //TODO: Hacer la consulta a SQL según los filtros



        //Debug.Log(TileMapManager.tileMap.filteredList[0]);
    }

    public void TestTilePos()
    {
        // Temp
        Vector3 pos = new Vector3();
        pos = tempCube.transform.position;

        // To Do: Hacer una funcion que itere por todas las posiciones y haga cosas con los GOs
        int index = GetTileIndex.GetTileIndexFromPosition(pos, TileMapManager.tileMap.rows, TileMapManager.tileMap.columns, startX - (tileSize.x / 2), startZ + (tileSize.z / 2), tileSize);

        Color baseColor = Color.red;
        Material randomMaterial = new Material(Shader.Find("Standard"));
        randomMaterial.color = baseColor;
        tiles[index].tileGO.GetComponent<Renderer>().material = randomMaterial;

        // Sumar 1 al Heat
        TileStruct tempTile = tiles[index];
        tempTile.heat++;                  
        tiles[index] = tempTile;

        Debug.Log("Tile: " + index + "Calor: " + tiles[index].heat);


        // Recorrer Lista de Vector3 Position -> tiles[index].Var 

        // Hacer un sort para ver la Var mas grande

        // Cuando sabemos la Var mas grande = tamaño max y color max

        // Se normalizan las demas vars dividiendo Var/Var es tamaño max

        // Cambiar las propiedades de los GO en funcion del Var normalizado
    }
}