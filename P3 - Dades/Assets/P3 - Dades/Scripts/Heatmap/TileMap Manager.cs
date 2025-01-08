using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileMapManager : MonoBehaviour
{
    [HideInInspector] public Color myColor = Color.white;  // Propiedad pública de tipo Color


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

    [HideInInspector] public int selectedDataIndex;
    #endregion

    //Filter Settings object
    public static FilterSettings filterSettings;

    // TileMap Data
    public static TileMap tileMap;

    [SerializeField] private GameObject tilePrefab;

    [Space(1.5f)]

    [Header("Tile Settings")]

    // Grid Vars
    [SerializeField, Range(1, 100)] int rows;
    [SerializeField, Range(1, 100)] int columns;

    // HeatMap Size Vars
    [SerializeField, Range(1, 1000)] int mapX;
    [SerializeField, Range(1, 1000)] int mapZ;

    private GameObject parent;
    private EditHeatMap heatMap;

    public void InitTool()
    {
        if (parent == null)
        {
            parent = GameObject.Find("Tiles Parent");
            heatMap = parent.GetComponent<EditHeatMap>();
        }

        tileMap.prefab = tilePrefab;
        tileMap.parent = parent;
        tileMap.rows = rows;
        tileMap.columns = columns;
        tileMap.width = mapX;
        tileMap.height = mapZ;
        tileMap.myColor = myColor;
        if (tileMap.filteredList == null)
        {
            tileMap.filteredList = new List<IDatabaseEntity>();
        }

        heatMap.InitTool();
    }
    public void GenerateTiles()
    {
        heatMap.GenerateTiles();
    }
    public void DeleteTiles()
    {
        heatMap.DeleteTiles();
    }

    public void ApplyFilters()
    {        
        //Check if any filter is being used
        #region Filters
        if (applyCountryFilter)
        {
            filterSettings.selectedCountryIndex = selectedCountryIndex;
            filterSettings.activeFilters = filterSettings.activeFilters | FilterType.Country;
        }

        if (applyGenderFilter)
        {
            filterSettings.selectedGenderIndex = selectedGenderIndex;
            filterSettings.activeFilters = filterSettings.activeFilters | FilterType.Gender;
        }

        if (applyAgeFilter)
        {
            filterSettings.minAge = minAge;
            filterSettings.maxAge = maxAge;
            filterSettings.activeFilters = filterSettings.activeFilters | FilterType.Age;
        }
        #endregion

        //Linkear selectedDataIndex del struct
        filterSettings.selectedDataIndex = selectedDataIndex;


        heatMap.ApplyFilters(filterSettings);

        //Reset activeFilters
        filterSettings.activeFilters = FilterType.None;

    }

    public void ExecuteHeatMapTool()
    {
        InitTool();
        GenerateTiles();
        ApplyFilters();
    }
}
