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

        if (GUILayout.Button("Generate Tiles"))
        {
            // Delete Tiles
            myComponent.GenerateTiles();
        }

        // Add Delete Button
        if (GUILayout.Button("Delete Tiles"))
        {
            // Delete Tiles
            myComponent.DeleteTiles();
        }
    }
}
