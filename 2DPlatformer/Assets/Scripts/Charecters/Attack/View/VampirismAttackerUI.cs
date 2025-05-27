using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VampirismAttackerUI : MonoBehaviour
{
    [SerializeField] private Player _player;

    private ValueAnimator _valueAnimator;

    private readonly float _maxValue = 1f;

    private Slider _slider;
    private float _value;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _value = 1f;
    }

    private void OnEnable()
    {
        _valueAnimator = new ValueAnimator(this);
        _player.VampirismIsReloading += IncreaseValue;
        _player.VampirismAttacked += DecreaseValue;
    }

    private void OnDisable()
    {
        _player.VampirismIsReloading -= IncreaseValue;
        _player.VampirismAttacked -= DecreaseValue;
    }

    private void IncreaseValue(float startValue)
    {
        startValue = Mathf.Clamp01(startValue);
        _valueAnimator.Animate((value) => _slider.value = value, startValue, _maxValue, _player.VampirismAttackReloadTime);
        _value = _maxValue;
    }

    private void DecreaseValue()
    {
        float startValue = _value / _maxValue;
        _valueAnimator.Animate((value) => _slider.value = value, startValue, 0f, _player.VampirismAttackTime);
        _value = 0f;
    }
}
