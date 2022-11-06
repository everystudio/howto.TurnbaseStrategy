using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    private float totalSpinAmount;
    private int maxShootDistance = 7;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += Vector3.up * spinAddAmount;

        totalSpinAmount += spinAddAmount;
        if (360f <= totalSpinAmount)
        {
            isActive = false;
            onActionComplete();
        }
    }
    public override string GetActionName()
    {
        return "SHOOT";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (maxShootDistance < testDistance)
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // ユニットが居ない場合は対象にならない
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetAnyUnitOnGridPosition(testGridPosition);
                if (unit.IsEnemy() == targetUnit.IsEnemy())
                {
                    // 同じチーム
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        totalSpinAmount = 0f;
    }
}
