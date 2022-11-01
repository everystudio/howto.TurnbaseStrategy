using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var startPos = GetWorldPosition(x, z);
                var endPos = startPos + Vector3.right * .2f;
                Debug.DrawLine(startPos, endPos, Color.white, 1000f);
            }
        }
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0f, z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
            );
    }


}
