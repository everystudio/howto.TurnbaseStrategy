using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTurnSystem : StateMachineBase<PlayerTurnSystem>
{
    private void Start()
    {
        ChangeState(new PlayerTurnSystem.Idle(this));
    }

    private class Idle : StateBase<PlayerTurnSystem>
    {
        private Unit selectedUnit;
        private GridPosition? selectedGridPosition;
        public Idle(PlayerTurnSystem machine) : base(machine)
        {
        }
        public override void OnEnterState()
        {
            MouseWorld.OnCurrentGridChanged += MouseWorld_OnCurrentGridChanged;
        }

        public override void OnUpdate()
        {
            if (selectedUnit != null && InputManager.Instance.IsMouseButtonDownThisFrame())
            {
                ChangeState(new PlayerTurnSystem.UnitSelect(machine, selectedUnit));
            }
        }

        private void MouseWorld_OnCurrentGridChanged(object sender, GridPosition gridPosition)
        {
            if (LevelGrid.Instance.IsValidGridPosition(gridPosition))
            {
                selectedUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
            }
            else
            {
                selectedUnit = null;
                selectedGridPosition = null;
            }
        }

        public override void OnExitState()
        {
            MouseWorld.OnCurrentGridChanged -= MouseWorld_OnCurrentGridChanged;
        }
    }

    private class UnitSelect : StateBase<PlayerTurnSystem>
    {
        private Unit unit;
        public UnitSelect(PlayerTurnSystem machine, Unit selectedUnit) : base(machine)
        {
            unit = selectedUnit;
        }
        public override void OnEnterState()
        {
            // 選択されたユニットの行動受付
            MouseWorld.OnCurrentGridChanged += MouseWorld_OnCurrentGridChanged; ;
        }

        private void MouseWorld_OnCurrentGridChanged(object sender, GridPosition e)
        {
        }
    }
}
