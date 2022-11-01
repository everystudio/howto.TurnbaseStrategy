using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArr;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArr = new GridObject[this.width, this.height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                gridObjectArr[x, z] = new GridObject(this, new GridPosition(x, z));
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

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.identity);
            }
        }
    }

}
