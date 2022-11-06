using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;
    private BaseAction selectedAction;

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

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        // ボタンの後ろでユニット選択とかができるのを防ぐ
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (!TryHandleUnitSelection() && selectedUnit == null)
        {
            return;
        }
        if (selectedAction != null)
        {
            HandleSelectedAction();
        }
    }

    private void HandleSelectedAction()
    {
        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());

        if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
        {
            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
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
