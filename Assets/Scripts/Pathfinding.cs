using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }


    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;


    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private LayerMask obstaclesLayerMask;

    private int width;
    private int height;
    private float cellSize;
    private GridSystem<PathNode> gridSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            // クリアしておこう
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    public void Setup(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridSystem = new GridSystem<PathNode>(width, height, cellSize,
            (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

                float raycastOffsetDistance = 5f;
                /*
                if (Physics.Raycast(
                    worldPosition + Vector3.down * raycastOffsetDistance,
                    Vector3.up,
                    raycastOffsetDistance * 2f,
                    obstaclesLayerMask))
                {
                    if (TryGetNode(x, z, out PathNode pathNode))
                    {
                        pathNode.SetIsWalkable(false);
                    }
                }
                */

                if (Physics.BoxCast(
                    worldPosition + Vector3.down * raycastOffsetDistance,
                    Vector3.one * cellSize * 0.25f, Vector3.up, Quaternion.identity,
                    raycastOffsetDistance * 2f, obstaclesLayerMask))
                {
                    if (TryGetNode(x, z, out PathNode pathNode))
                    {
                        pathNode.SetIsWalkable(false);
                    }
                }

            }
        }

    }

    public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = gridSystem.GetGridObject(startGridPosition);
        PathNode endNode = gridSystem.GetGridObject(endGridPosition);
        openList.Add(startNode);

        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                PathNode pathNode = gridSystem.GetGridObject(gridPosition);

                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startGridPosition, endGridPosition));
        startNode.CalculateFCost();

        while (0 < openList.Count)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if (currentNode == endNode)
            {
                // 目的のノードの到達しました
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (!neighbourNode.IsWalkable())
                {
                    closedList.Add(neighbourNode);
                }

                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }


                int tentativeGCost = currentNode.GetGCost() + CalculateDistance(
                    currentNode.GetGridPosition(), neighbourNode.GetGridPosition());

                if (tentativeGCost < neighbourNode.GetGCost())
                {
                    neighbourNode.SetCameFromPathNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistance(
                        neighbourNode.GetGridPosition(),
                        endGridPosition));
                    neighbourNode.CalculateFCost();
                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        // パス見つからず
        return null;
    }

    public int CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB)
    {
        GridPosition gridPositionDistance = gridPositionA - gridPositionB;
        int distance = Mathf.Abs(gridPositionDistance.x) + Mathf.Abs(gridPositionDistance.z);
        return distance * MOVE_STRAIGHT_COST;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];

        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }
        return lowestFCostPathNode;
    }

    private bool TryGetNode(int x, int z, out PathNode pathNode)
    {
        pathNode = null;
        if ((0 <= x && x < gridSystem.GetWidth()) &&
           (0 <= z && z < gridSystem.GetHeight()))
        {
            pathNode = gridSystem.GetGridObject(new GridPosition(x, z));
            return true;
        }
        return false;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        GridPosition gridPosition = currentNode.GetGridPosition();

        // left
        Vector2Int[] offsetArray = new Vector2Int[4]
        {
            new Vector2Int(-1, 0),
            new Vector2Int( 0, 1),
            new Vector2Int( 1, 0),
            new Vector2Int( 0,-1),
        };
        for (int i = 0; i < offsetArray.Length; i++)
        {
            if (TryGetNode(
                gridPosition.x + offsetArray[i].x,
                gridPosition.z + offsetArray[i].y,
                out PathNode pathNode))
            {
                neighbourList.Add(pathNode);
            }
        }

        return neighbourList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new List<PathNode>();
        pathNodeList.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.GetCameFromPathNode() != null)
        {
            pathNodeList.Add(currentNode.GetCameFromPathNode());
            currentNode = currentNode.GetCameFromPathNode();
        }
        pathNodeList.Reverse();

        List<GridPosition> gridPositionList = new List<GridPosition>();
        foreach (PathNode pathNode in pathNodeList)
        {
            gridPositionList.Add(pathNode.GetGridPosition());
        }
        return gridPositionList;
    }
}
