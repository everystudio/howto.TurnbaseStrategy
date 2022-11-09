using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;

    private void Start()
    {
        Unit.OnAnyActionPointsChanged += (sender, e) =>
        {
            UpdateActionPointsText();
        };
        UpdateActionPointsText();
    }

    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }
}
