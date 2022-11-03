using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;

    private void Awake()
    {
        targetPosition = transform.position;
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        bool isWalking = false;
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float moveSpeed = 5f;
        Vector3 moveVector = moveDir * (moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) <= moveVector.magnitude)
        {
            transform.position = targetPosition;
        }
        else
        {
            isWalking = true;
            transform.position += moveVector;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        unitAnimator.SetBool("IsWalking", isWalking);

    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
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
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
