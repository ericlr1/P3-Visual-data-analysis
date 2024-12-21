using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileMapManager : MonoBehaviour
{
    [SerializeField, Range(1, 100)] int rows;
    [SerializeField, Range(1, 100)] int columns;
    private float f_Rows;
    private float f_Columns;

    [SerializeField, Range(1, 1000)] int mapX;
    [SerializeField, Range(1, 1000)] int mapZ;
    private float f_MapX;
    private float f_MapZ;


    private float tileSize_X;
    private float tileSize_Z;

    public GameObject parent;

    public GameObject tilePrefab;

    [SerializeField] private List<GameObject> tiles;

    GameObject[,] tilesPrefab;

    public void CalculateTiles() // Calculate Tile Data
    {
        // Delete all the tiles before create new ones
        DeleteTiles();

        f_Rows = (float)rows;
        f_Columns = (float)columns;

        f_MapX = (float)mapX;
        f_MapZ = (float)mapZ;

        tileSize_X = f_MapX / f_Rows;
        tileSize_Z = f_MapZ / f_Columns;

        Debug.Log("Rows: " + rows + " Columns: " + columns);
        Debug.Log("tileSize_X: " + tileSize_X + " tileSize_Z: " + tileSize_Z);

        CreateTiles();
    }

    private void CreateTiles() // Create Tile Grid
    {
        Vector3 pos = new Vector3 (0, gameObject.transform.position.y, 0);

        pos.x = (gameObject.transform.position.x - (f_MapX / 2)) + (tileSize_X / 2);
        pos.z = (gameObject.transform.position.z + (f_MapZ / 2)) - (tileSize_Z / 2);

        for (int i = 0; i < columns; i++)
        {
            if (i != 0)
            {
                pos.x = (gameObject.transform.position.x - (f_MapX / 2)) + (tileSize_X / 2);
                pos.z -= tileSize_Z;
            }
            
            for (int j = 0; j < rows; j++)
            {
                if (j != 0)
                {
                    pos.x += tileSize_X;
                }
                
                GameObject tile = Instantiate(tilePrefab, parent.transform);

                tile.name = "Tile " + i + "_" + j;
                tile.transform.position = pos;
                tile.transform.localScale = new Vector3(tileSize_X, 1, tileSize_Z);

                Color randomColor = new Color(Random.value, Random.value, Random.value);
                Material randomMaterial = new Material(Shader.Find("Standard"));
                randomMaterial.color = randomColor;
                tile.GetComponent<Renderer>().material = randomMaterial;

                tiles.Add(tile);
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

        tiles.Clear();

        for (int x = tilesPrefab.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = tilesPrefab.GetLength(1) - 1; y >= 0; y--)
            {
                tilesPrefab[x, y] = null;
            }
        }
    }
}
