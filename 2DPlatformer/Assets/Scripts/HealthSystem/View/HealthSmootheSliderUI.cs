using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthSmootheSliderUI : HealthSliderUI
{
    [SerializeField] private float _animationTime;

    private ValueAnimator _valueAnimator;

    public override void ChangeValue(float value)
    {
        float finalValue = value / MaxValue;
        float startValue = Value / MaxValue;
        _valueAnimator.Animate((value) => Slider.value = value, startValue, finalValue, _animationTime);
        Value = value;
    }

    protected override void Register()
    {
        _valueAnimator = new ValueAnimator(this);
    }
}
