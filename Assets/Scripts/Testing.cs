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
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(
                unit.GetAction<MoveAction>().GetValidActionGridPositionList(),
                GridSystemVisual.GridVisualType.White);
        }
    }
}
