using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileMapManager : MonoBehaviour
{
    [SerializeField, Range(1, 100)] int rows;
    [SerializeField, Range(1, 100)] int columns; 

    [SerializeField, Range(1, 1000)] int mapX;
    [SerializeField, Range(1, 1000)] int mapZ;

    private float tileSize_X;
    private float tileSize_Z;

    public GameObject parent;

    public GameObject tilePrefab;

    public void CalculateTiles() // Calculate Tile Data
    {
        // Delete all the tiles before create new ones
        DeleteTiles();

        tileSize_X = mapX / rows;
        tileSize_Z = mapZ / columns;

        Debug.Log("Rows: " + rows + " Columns: " + columns);
        Debug.Log("tileSize_X: " + tileSize_X + " tileSize_Z: " + tileSize_Z);

        CreateTiles();
    }

    private void CreateTiles() // Create Tile Grid
    {
        Vector3 pos = new Vector3 (0, 0, 0);

        pos.x = gameObject.transform.position.x - (mapX / 2) + (tileSize_X / 2);
        pos.z = gameObject.transform.position.z - (mapZ / 2) + (tileSize_Z / 2);

        for (int i = 0; i < rows; i++)
        {
            pos.x = gameObject.transform.position.x - (mapX / 2) + (tileSize_X / 2);
            pos.z += tileSize_Z;

            for (int j = 0; j < columns; j++)
            {
                pos.x += tileSize_X;

                GameObject tile = Instantiate(tilePrefab, parent.transform);

                tile.transform.position = pos;
                tile.transform.localScale = new Vector3(tileSize_X, 1, tileSize_Z);

                Color randomColor = new Color(Random.value, Random.value, Random.value);
                Material randomMaterial = new Material(Shader.Find("Standard"));
                randomMaterial.color = randomColor;
                tile.GetComponent<Renderer>().material = randomMaterial;
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
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
