using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthSliderUI : HealthUI
{
    protected Slider Slider;

    private void Awake() =>
        Slider = GetComponent<Slider>();

    public override void ChangeValue(float value)
    {
        float finalValue = value / MaxValue;
        Slider.value = finalValue;
    }
}
