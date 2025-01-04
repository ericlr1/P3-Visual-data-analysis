using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TileMapManager))]
public class MyComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var defaultColor = GUI.color;

        Separator();

        SetTiltle("Edit HeatMap Size");

        DrawDefaultInspector(); // Default Inspector GUI
        
        TileMapManager myComponent = (TileMapManager)target; // Reference to the script overrided

        Separator(5);

        ChangeUIColor(Colors.MintGreen);

        // Generate Tiles
        if (GUILayout.Button("Generate Tiles"))
        {
            myComponent.GenerateTiles();
        }

        ChangeUIColor(Colors.Crimson);
        // Delete Tiles
        if (GUILayout.Button("Delete Tiles"))
        {
            myComponent.DeleteTiles();
        }

        ChangeUIColor(defaultColor);

        Separator();

        SetTiltle("Filter Options");

        // Country Filter
        #region Country Filter
        myComponent.applyCountryFilter = EditorGUILayout.Toggle("Country", myComponent.applyCountryFilter);

        if (myComponent.applyCountryFilter)
        {
            myComponent.selectedCountryIndex = EditorGUILayout.Popup(myComponent.selectedCountryIndex, UserAttributes.GetCountryArray());
        }
        else
        {
            myComponent.selectedCountryIndex = -1;
        }
        #endregion

        // Gender Filter
        #region Gender Flilter
        myComponent.applyGenderFilter = EditorGUILayout.Toggle("Gender", myComponent.applyGenderFilter);

        if (myComponent.applyGenderFilter)
        {
            myComponent.selectedGenderIndex = EditorGUILayout.Popup(myComponent.selectedGenderIndex, UserAttributes.GetGenderArray());
        }
        else
        {
            myComponent.selectedGenderIndex = -1;
        }
        #endregion

        // Age Filter
        #region Age Filter
        myComponent.applyAgeFilter = EditorGUILayout.Toggle("Age", myComponent.applyAgeFilter);

        if (myComponent.applyAgeFilter)
        {
            // Min Age Slider
            myComponent.minAge = Mathf.RoundToInt(
                EditorGUILayout.Slider("Min Age", myComponent.minAge, 0, 100)
            );

            // Max Age Slider
            myComponent.maxAge = Mathf.RoundToInt(
                EditorGUILayout.Slider("Max Age", myComponent.maxAge, 0, 100)
            );

            // Avoid Min age being above Min Age
            if (myComponent.minAge > myComponent.maxAge)
            {
                myComponent.minAge = myComponent.maxAge;
            }

            // Avoid Max age being below Min Age
            if (myComponent.maxAge < myComponent.minAge)
            {
                myComponent.maxAge = myComponent.minAge;
            }

            // Apply changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(myComponent);
            }
        }
        #endregion

        Separator();

        // Apply filters button
        if (GUILayout.Button("Apply Filters"))
        {
            myComponent.ApplyFilters();
        }

    }

    #region UI Funcions
    private void ChangeUIColor(Color color)
    {
        GUI.color = color;
    }
    private void SetTiltle(string name)
    {
        EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
    }

    private void Separator(float space = 10)
    {
        GUILayout.Space(space);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Space(space);
    }
    #endregion
}
