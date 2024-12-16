using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileMapManager))] // Indica que este editor es para MyComponent
public class MyComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Default Inspector GUI
        DrawDefaultInspector();

        // Reference to the script overrided
        TileMapManager myComponent = (TileMapManager)target;

        // Add Generate Tile Button
        if (GUILayout.Button("Generate Tiles"))
        {
            myComponent.CalculateTiles();
        }

        // Add Delete Button
        if (GUILayout.Button("Delete Tiles"))
        {
            // Delete Tiles
            myComponent.DeleteTiles();
        }
    }
}
