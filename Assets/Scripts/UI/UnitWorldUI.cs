using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Healthsystem healthsystem;

    private void Start()
    {
        Unit.OnAnyActionPointsChanged += (sender, e) =>
        {
            UpdateActionPointsText();
        };
        healthsystem.OnDamaged += (sender, e) =>
        {
            UpdateHealthBar();
        };
        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthsystem.GetHealthNormalized();
    }
}
