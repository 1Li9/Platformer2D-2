using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthTextUI : HealthUI
{
    private TextMeshProUGUI _text;

    private void Awake() =>
        _text = GetComponent<TextMeshProUGUI>();

    public override void ChangeValue(float value)
    {
        Value = value;
        float displayValue = Mathf.Round(Value);
        _text.text = $"{displayValue} / {MaxValue}";
    }
}