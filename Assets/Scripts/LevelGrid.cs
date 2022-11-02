using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem gridSystem;
    private void Awake()
    {
        gridSystem = new GridSystem(10, 10, 2f);

        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

    }

    // Challange
    // 下３つのメソッドを完成させてみよう
    // GridObjectにユニットをセットしたりゲットしたりするメソッドを追加しましょう
    // GridObject.ToStringを変更して、Unitがいるかどうかを確認できるようにしましょう
    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
    }
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        return null;
    }
    public void ClearUnitAtGridPosition(GridPosition gridPosition)
    {
    }

}
