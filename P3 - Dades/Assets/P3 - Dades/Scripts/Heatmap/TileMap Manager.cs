using System;
using System.Collections.Generic;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    // Tile colors
    [HideInInspector] public Color myColor = Color.white;
    [HideInInspector] public Color myBaseColor = Color.white;

    [HideInInspector] public int filteredTileScale = 10;

    #region Filter Options
    // Country filter variables
    [HideInInspector] public bool applyCountryFilter;
    [HideInInspector] public int selectedCountryIndex;

    // Gender filter variables
    [HideInInspector] public bool applyGenderFilter;
    [HideInInspector] public int selectedGenderIndex;

    // Age filter variables
    [HideInInspector] public bool applyAgeFilter;
    [HideInInspector] public int minAge = 0;
    [HideInInspector] public int maxAge = 100;

    // General filter data
    [HideInInspector] public int selectedDataIndex;
    #endregion

    // Static filter settings
    public static FilterSettings filterSettings;

    // Static tile map data
    public static TileMap tileMap;

    // Prefab for tiles
    [SerializeField] private GameObject tilePrefab;

    // Tile settings
    [Header("Edit HeatMap Size")]
    [Space(5)]
    [SerializeField, Range(1, 100)] int rows; // Number of rows
    [SerializeField, Range(1, 100)] int columns; // Number of columns
    [SerializeField, Range(1, 1000)] int mapX; // Map width
    [SerializeField, Range(1, 1000)] int mapZ; // Map height

    // Internal references
    private GameObject parent;
    private EditHeatMap heatMap;

    // Number of filtered users
    public static int filteredUsers = 0;

    // Initialize the tool and set up the tile map
    public void InitTool()
    {
        if (parent == null)
        {
            parent = GameObject.Find("Tiles Parent");
            heatMap = parent.GetComponent<EditHeatMap>();
        }

        // Set up tile map properties
        tileMap.prefab = tilePrefab;
        tileMap.parent = parent;
        tileMap.rows = rows;
        tileMap.columns = columns;
        tileMap.width = mapX;
        tileMap.height = mapZ;
        tileMap.myColor = myColor;
        tileMap.myBaseColor = myBaseColor;
        tileMap.filteredTileScale = filteredTileScale;

        if (tileMap.filteredList == null)
        {
            tileMap.filteredList = new List<IDatabaseEntity>();
        }

        // Initialize heat map tool
        heatMap.InitTool();
    }

    // Generate the tiles in the heat map
    public void GenerateTiles()
    {
        heatMap.GenerateTiles();
    }

    // Delete the existing tiles
    public void DeleteTiles()
    {
        heatMap.DeleteTiles();
    }

    // Apply the selected filters
    public void ApplyFilters()
    {
        // Check and apply country filter
        if (applyCountryFilter)
        {
            filterSettings.selectedCountryIndex = selectedCountryIndex;
            filterSettings.activeFilters |= FilterType.Country;
        }

        // Check and apply gender filter
        if (applyGenderFilter)
        {
            filterSettings.selectedGenderIndex = selectedGenderIndex;
            filterSettings.activeFilters |= FilterType.Gender;
        }

        // Check and apply age filter
        if (applyAgeFilter)
        {
            filterSettings.minAge = minAge;
            filterSettings.maxAge = maxAge;
            filterSettings.activeFilters |= FilterType.Age;
        }

        // Link selected data index
        filterSettings.selectedDataIndex = selectedDataIndex;

        // Apply filters to the heat map
        heatMap.ApplyFilters(filterSettings);

        // Reset active filters
        filterSettings.activeFilters = FilterType.None;
    }

    // Execute the complete heat map tool process
    public void ExecuteHeatMapTool()
    {
        InitTool(); // Initialize the tool
        GenerateTiles(); // Generate tiles
        ApplyFilters(); // Apply filters
    }
}
