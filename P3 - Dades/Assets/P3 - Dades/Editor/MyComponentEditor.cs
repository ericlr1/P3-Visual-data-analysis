using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


[CustomEditor(typeof(TileMapManager))]
public class MyComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        string[] dataList =
        {
            "player_respawns",
            "player_positions",
            "player_jumps",
            "player_interactions",
            "player_hits",
            "player_heals",
            "player_deaths",
            "player_damaged"
        };

        // ------------------- Tool Tilte ------------------- \\
        #region Tool Tilte
        Separator(5);

        SetLargeTitle("HEATMAP TOOL");

        Separator();
        #endregion

        DrawClickableLink("Visit GitHub Repository", "https://github.com/ericlr1/P3-Visual-data-analysis");

        GUILayout.Space(5);

        // ------------------- Edit HeatMap ------------------- \\
        #region Edit HeatMap
        SetTitle("Edit HeatMap Size");

        GUILayout.Space(5);

        DrawDefaultInspector(); // Default Inspector GUI
        
        TileMapManager myComponent = (TileMapManager)target; // Reference to the script overrided

        Separator(5);
        #endregion

        // ------------------- Select Color ------------------- \\
        #region Select Color
        SetTitle("Select HeatMap Color");

        GUILayout.Space(5);

        myComponent.myBaseColor = EditorGUILayout.ColorField("Background Color", myComponent.myBaseColor);

        GUILayout.Space(5);

        myComponent.myColor = EditorGUILayout.ColorField("Tile Color", myComponent.myColor);
       
        Separator();
        #endregion

        // ------------------- Data Selector ------------------- \\
        #region Data Selector
        SetTitle("Data Selector");

        GUILayout.Space(5);

        myComponent.selectedDataIndex = EditorGUILayout.Popup(myComponent.selectedDataIndex, dataList);

        Separator();
        #endregion

        // ------------------- Filter Options ------------------- \\
        SetTitle("Filter Options");

        GUILayout.Space(5);

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

        Separator(5);

        SetTitle("Filtered Data Sample: " + TileMapManager.filteredUsers);

        GUILayout.Space(10);

        ChangeUIColor(Colors.MintGreen);

        if (GUILayout.Button("Generate Tiles"))
        {
            myComponent.ExecuteHeatMapTool();
        }

        GUILayout.Space(5);

        ChangeUIColor(Colors.Crimson);
        // Delete Tiles
        if (GUILayout.Button("Delete Tiles"))
        {
            myComponent.DeleteTiles();
        }

    }

    #region UI Funcions
    private void ChangeUIColor(Color color)
    {
        GUI.color = color;
    }
    private void SetTitle(string name)
    {
        EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
    }

    private void Separator(float space = 10)
    {
        GUILayout.Space(space);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Space(space);
    }

    private void SetLargeTitle(string name, int size = 24)
    {
        var style = new GUIStyle(EditorStyles.boldLabel);
        style.fontSize = size;  // Change font size
        style.alignment = TextAnchor.MiddleCenter; // Center text
        EditorGUILayout.LabelField(name, style);
    }

    private void DrawClickableLink(string label, string url)
    {
        var style = new GUIStyle(EditorStyles.label)
        {
            normal = { textColor = Colors.RoyalBlue },
            hover = { textColor = Colors.SteelBlue },
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        if (GUILayout.Button(label, style))
        {
            Application.OpenURL(url);
        }
    }
    #endregion
}
