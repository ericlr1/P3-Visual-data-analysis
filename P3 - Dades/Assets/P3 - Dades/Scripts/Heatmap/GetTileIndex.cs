using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetTileIndex
{
    public static int GetTileIndexFromPosition(Vector3 point, int rows, int columns, float startX, float startZ, Vector3 tileSize)
    {
        // Calculates the local coordinates of the point with respect to the grid start point
        float localX = point.x - startX;
        float localZ = point.z - startZ;

        // Calculate tile coordinates
        int _column = Mathf.FloorToInt(localX / tileSize.x);
        int _row = Mathf.CeilToInt(localZ / tileSize.z);

        // Check if its in the bounds
        if (_column < 0 || _column >= columns || -_row < 0 || -_row >= rows)
        {
            return -1; // Return -1 if its out of bounds
        }

        // Final Index
        return -_row * columns + _column;
    }
}
