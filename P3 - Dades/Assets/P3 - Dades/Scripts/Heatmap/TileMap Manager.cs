using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileMapManager : MonoBehaviour
{
    #region Filter Options
    // Country Filter Vars
    [HideInInspector] public bool applyCountryFilter;
    [HideInInspector] public int selectedCountryIndex;

    // Gender Filter Vars
    [HideInInspector] public bool applyGenderFilter;
    [HideInInspector] public int selectedGenderIndex;

    // Age Filter Vars
    [HideInInspector] public bool applyAgeFilter;
    [HideInInspector] public int minAge = 0;
    [HideInInspector] public int maxAge = 100;
    #endregion

    // TileMap Data
    public static TileMap tileMap;

    [SerializeField] private DatabaseReceive dataRecieve;

    [SerializeField] private GameObject tilePrefab;

    // Grid Vars
    [SerializeField, Range(1, 100)] int rows;
    [SerializeField, Range(1, 100)] int columns;

    // HeatMap Size Vars
    [SerializeField, Range(1, 1000)] int mapX;
    [SerializeField, Range(1, 1000)] int mapZ;

    private GameObject parent;
    private EditHeatMap heatMap;

    public void GenerateTiles()
    {
        
        if (parent == null)
        {
            parent = GameObject.Find("Tiles Parent");
            heatMap = parent.GetComponent<EditHeatMap>();
        }

        tileMap.filteredList = new List<IDatabaseEntity>();
        tileMap.prefab = tilePrefab;
        tileMap.parent = parent;
        tileMap.rows = rows;
        tileMap.columns = columns;
        tileMap.width = mapX;
        tileMap.height = mapZ;

        tileMap.filteredList.AddRange(dataRecieve.dbPlayerInteractions);
        tileMap.filteredList.AddRange(dataRecieve.dbUsers);
        tileMap.filteredList.AddRange(dataRecieve.dbPlayerDamages);

        heatMap.GenerateTiles();
    }
    public void DeleteTiles()
    {
        heatMap.DeleteTiles();
    }
}
