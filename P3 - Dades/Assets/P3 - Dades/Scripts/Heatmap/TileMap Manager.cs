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

    //Filter Settings object
    public static FilterSettings filterSettings;

    // TileMap Data
    public static TileMap tileMap;

    [SerializeField] private GameObject tilePrefab;

    // Grid Vars
    [SerializeField, Range(1, 100)] int rows;
    [SerializeField, Range(1, 100)] int columns;

    // HeatMap Size Vars
    [SerializeField, Range(1, 1000)] int mapX;
    [SerializeField, Range(1, 1000)] int mapZ;

    private GameObject parent;
    private EditHeatMap heatMap;

    bool tilesCreated = false;

    public void GenerateTiles()
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
        if(tileMap.filteredList == null)
        {
            tileMap.filteredList = new List<IDatabaseEntity>();
        }

        //TODO: Delete this
        //tileMap.filteredList.AddRange(dataRecieve.dbPlayerInteractions);
        //tileMap.filteredList.AddRange(dataRecieve.dbUsers);
        //tileMap.filteredList.AddRange(dataRecieve.dbPlayerDamages);

        heatMap.GenerateTiles();
        tilesCreated = true;
    }
    public void DeleteTiles()
    {
        heatMap.DeleteTiles();
        tilesCreated = false;
    }

    public void ApplyFilters()
    {
        //Si nunca se han generado als tiles hacerlo con los valores por defecto
        if (!tilesCreated)
        {
            GenerateTiles();
        }

        if (tileMap.filteredList == null)
        {
            tileMap.filteredList = new List<IDatabaseEntity>();
        }
        
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

        heatMap.ApplyFilters(filterSettings);

        //Reset activeFilters
        filterSettings.activeFilters = FilterType.None;

    }

    public void TestIndex()
    {
        heatMap.TestTilePos();
    }
}
