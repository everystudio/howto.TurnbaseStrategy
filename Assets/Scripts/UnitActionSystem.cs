using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;

    private void Awake()
    {
        if (Instance == null)
        {
            // クリアしておこう
            selectedUnit = null;
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!TryHandleUnitSelection() && selectedUnit != null)
            {
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy();
                    selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }

    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.collider.TryGetComponent<Unit>(out Unit unit))
            {
                if (selectedUnit != unit)
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
