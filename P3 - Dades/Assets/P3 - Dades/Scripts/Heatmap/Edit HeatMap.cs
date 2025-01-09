using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class EditHeatMap : MonoBehaviour
{
    // Tile Variables
    private Vector3 tileSize;

    private float startX;
    private float startZ;

    // Manager Vars
    private GameObject parent;
    public List<TileStruct> tiles;

    // Database Object
    [SerializeField] private DatabaseReceive dataRecieve;

    [HideInInspector]
    private Dictionary<int, List<IDatabaseEntity>> _dataDictionary;
    public Dictionary<int, List<IDatabaseEntity>> DataDictionary
    {
        get
        {
            if (_dataDictionary == null)
            {
                _dataDictionary = new Dictionary<int, List<IDatabaseEntity>>
            {
                { 0, dataRecieve.dbPlayerRespawnsView.Cast<IDatabaseEntity>().ToList() },
                { 1, dataRecieve.dbPlayerPositionsView.Cast<IDatabaseEntity>().ToList() },
                { 2, dataRecieve.dbPlayerJumpsView.Cast<IDatabaseEntity>().ToList() },
                { 3, dataRecieve.dbPlayerInteractionsView.Cast<IDatabaseEntity>().ToList() },
                { 4, dataRecieve.dbPlayerHitsView.Cast<IDatabaseEntity>().ToList() },
                { 5, dataRecieve.dbPlayerHealsView.Cast<IDatabaseEntity>().ToList() },
                { 6, dataRecieve.dbPlayerDeathsView.Cast<IDatabaseEntity>().ToList() },
                { 7, dataRecieve.dbPlayerDamagesView.Cast<IDatabaseEntity>().ToList() }
            };
            }
            return _dataDictionary;
        }
    }

    public void InitTool()
    {
        //Recive data
        dataRecieve.ReceiveDataButtonCorrutine();

        tiles = new List<TileStruct>();

        if (parent == null)
        {
            parent = TileMapManager.tileMap.parent;
        }
    }

    public void GenerateTiles()
    {
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

                // Change the tile size
                tile.tileGO.transform.localScale = new Vector3(tileSize.x / tile.tileGO.transform.localScale.x, tile.tileGO.transform.localScale.y, tileSize.z / tile.tileGO.transform.localScale.z);

                Color baseColor = TileMapManager.tileMap.myBaseColor;
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
        // Empty the list
        TileMapManager.tileMap.filteredList.Clear();

        TileMapManager.tileMap.filteredList.AddRange(DataDictionary[TileMapManager.filterSettings.selectedDataIndex]);


        if ((filterSettings.activeFilters & FilterType.Country) != 0)
        {
            int count = TileMapManager.tileMap.filteredList.Count();

            for (int i = count - 1; i >= 0; --i)
            {
                if (TileMapManager.tileMap.filteredList[i].country != UserAttributes.GetCountryArray()[filterSettings.selectedCountryIndex])
                {
                    TileMapManager.tileMap.filteredList.RemoveAt(i);
                }
            }
        }

        if ((filterSettings.activeFilters & FilterType.Gender) != 0)
        {
            int count = TileMapManager.tileMap.filteredList.Count();

            for (int i = count - 1; i >= 0; --i)
            {
                if (TileMapManager.tileMap.filteredList[i].gender != UserAttributes.GetGenderArray()[filterSettings.selectedGenderIndex])
                {
                    TileMapManager.tileMap.filteredList.RemoveAt(i);
                }
            }
        }

        if ((filterSettings.activeFilters & FilterType.Age) != 0)
        {
            int count = TileMapManager.tileMap.filteredList.Count();

            for (int i = count - 1; i >= 0; --i)
            {
                if (TileMapManager.tileMap.filteredList[i].age < filterSettings.minAge || TileMapManager.tileMap.filteredList[i].age > filterSettings.maxAge)
                {
                    TileMapManager.tileMap.filteredList.RemoveAt(i);
                }
            }
        }

        TileMapManager.filteredUsers = TileMapManager.tileMap.filteredList.Count;
        
        // Change the color and scale to the tiles
        DrawFilteredTiles();

    }

    public void DrawFilteredTiles()
    {
        // Check if the list is empty
        if (TileMapManager.tileMap.filteredList.Count() == 0)
        {
            return;
        }

        // Recibe data
        foreach (IDatabaseEntity entity in TileMapManager.tileMap.filteredList)
        {
            Vector3 pos = new Vector3();
            pos.x = entity.x;
            pos.y = entity.y;
            pos.z = entity.z;

            int index = GetTileIndex.GetTileIndexFromPosition(pos, TileMapManager.tileMap.rows, TileMapManager.tileMap.columns, startX - (tileSize.x / 2), startZ + (tileSize.z / 2), tileSize);
            
            // Increment Heat
            TileStruct tempTile = tiles[index];
            tempTile.heat++;
            tiles[index] = tempTile;
        }

        // Sort by Heat
        tiles.Sort((tile1, tile2) => tile2.heat.CompareTo(tile1.heat));

        float maxheatCoef = 1 / tiles[0].heat;

        // Edit tiles based on it's heat
        for (int i = 0; i < tiles.Count; i++)
        {
            Vector3 tileScale = new Vector3(tileSize.x, 1, tileSize.z);

            TileStruct tempTile = tiles[i];
            tempTile.heat *= maxheatCoef;
            tiles[i] = tempTile;

            Color tileColor = Color.Lerp(TileMapManager.tileMap.myBaseColor, TileMapManager.tileMap.myColor, tempTile.heat);

            Material randomMaterial = new Material(Shader.Find("Standard"));
            randomMaterial.color = tileColor;
            tiles[i].tileGO.GetComponent<Renderer>().material = randomMaterial;

            if (tempTile.heat != 0)
            {
                tileScale.y += (10 * tempTile.heat);

                tiles[i].tileGO.transform.localScale = tileScale;
            }
            else
            {
                tileScale.y = 1;
                tiles[i].tileGO.transform.localScale = tileScale;
            }
        }
    }
}