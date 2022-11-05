﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        bool isWalking = false;
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float moveSpeed = 5f;
        Vector3 moveVector = moveDir * (moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) <= moveVector.magnitude)
        {
            isActive = false;
            transform.position = targetPosition;
            onActionComplete();
        }
        else
        {
            isWalking = true;
            transform.position += moveVector;
        }
        unitAnimator.SetBool("IsWalking", isWalking);

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

    public void Move(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // すでに同グリッドにユニットがいる
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
    public override string GetActionName()
    {
        return "Move";
    }
}
