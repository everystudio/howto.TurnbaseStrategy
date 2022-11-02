using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    private Vector3 _targetPosition;
    private GridPosition gridPosition;

    private float _moveSpeed = 5f;

    private void Awake()
    {
        _targetPosition = transform.position;
    }
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    {
        bool isWalking = false;
        Vector3 moveDir = (_targetPosition - transform.position).normalized;
        Vector3 moveVector = moveDir * (_moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _targetPosition) <= moveVector.magnitude)
        {
            transform.position = _targetPosition;
        }
        else
        {
            isWalking = true;
            //transform.Translate(moveVector);
            transform.position += moveVector;
            //transform.forward = moveDir;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        unitAnimator.SetBool("IsWalking", isWalking);

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            // Unit changed Grid Position;
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }

    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
