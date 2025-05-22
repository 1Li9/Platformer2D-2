using System;

public class Health
{
    private IAnimator _animator;

    private float _value;
    private float _maxValue;

    public event Action<float> ValueChanged;
    public event Action<float> MaxValueChanged;

    public Health(IAnimator animator, float maxValue)
    {
        _animator = animator;
        _maxValue = maxValue;
        _value = _maxValue;
    }

    public float Value
    {
        get => _value;

        private set
        {
            _value = value;
            ValueChanged?.Invoke(_value);
        }
    }

    public float MaxValue
    {
        get => _maxValue;

        private set
        {
            _maxValue = value;
            MaxValueChanged?.Invoke(_maxValue);
        }
    }

    public bool IsAlive { get; private set; } = true;

    public void Initialize()
    {
        MaxValueChanged?.Invoke(MaxValue);
        ValueChanged?.Invoke(Value);
    }

    public void TakeDamage(float damage)
    {
        if (Value - damage <= 0)
        {
            Value = 0;
            IsAlive = false;
            _animator.UpdateDeadTrigger();

            return;
        }

        Value -= damage;
    }

    public void Increase(float healPoints)
    {
        if (Value + healPoints >= MaxValue)
        {
            Value = MaxValue;

            return;
        }

        Value += healPoints;
    }
}