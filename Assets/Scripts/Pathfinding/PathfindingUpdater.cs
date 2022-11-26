using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{
    private void Start()
    {
        DestructableCrate.OnAnyDestroyed += (sender, e) =>
        {
            DestructableCrate destructableCrate = sender as DestructableCrate;
            Pathfinding.Instance.SetIsWalkableGridPosition(destructableCrate.GetGridPosition(), true);
        };
    }
}
