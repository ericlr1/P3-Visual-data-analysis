using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DatabaseReceive))] // Indica que este editor es para MyComponent
public class DatabaseReceiveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Default Inspector GUI
        DrawDefaultInspector();

        // Reference to the script overrided
        DatabaseReceive myComponent = (DatabaseReceive)target;

        // Receive Data from Database
        if (GUILayout.Button("Receive Data"))
        {
            myComponent.ReceiveDataButton();
        }
    }
}
