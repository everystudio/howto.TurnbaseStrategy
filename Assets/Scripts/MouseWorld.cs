using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance;
    public static event EventHandler<GridPosition> OnCurrentGridChanged;

    public GridPosition CurrentGrid => currentGrid;
    private GridPosition currentGrid;

    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        Instance = this;
        currentGrid = new GridPosition(0, 0);
    }

    private void Update()
    {
        transform.position = GetPosition();
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(GetPosition());
        if (currentGrid != gridPosition)
        {
            currentGrid = gridPosition;
            OnCurrentGridChanged?.Invoke(this, gridPosition);
        }
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool bIsHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.groundLayerMask);
        return raycastHit.point;
    }
}
