using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
            GridPosition startGridPosition = new GridPosition(0, 0);

            List<GridPosition> gridPositionList = Pathfinding.Instance.FindPath(startGridPosition, mouseGridPosition, out int pathLength);
            Debug.Log(gridPositionList.Count);
            for (int i = 0; i < gridPositionList.Count - 1; i++)
            {
                Debug.DrawLine(
                    LevelGrid.Instance.GetWorldPosition(gridPositionList[i]) + Vector3.up * 0.02f,
                    LevelGrid.Instance.GetWorldPosition(gridPositionList[i + 1]) + Vector3.up * 0.02f,
                    Color.white, 10f
                    );
            }
        }
    }
}
