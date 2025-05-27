using UnityEngine;

public abstract class HealthUI : MonoBehaviour
{
    [SerializeField] private Charecter _charecter;

    protected float MaxValue;
    protected float Value;

    private void OnEnable()
    {
        Register();
        _charecter.HealthChanged += ChangeValue;
        _charecter.MaxHealthChanged += SetMaxValue;
    }

    private void OnDisable()
    {
        _charecter.HealthChanged -= ChangeValue;
        _charecter.MaxHealthChanged -= SetMaxValue;
    }

    public abstract void ChangeValue(float value);
    protected virtual void Register() { }

    private void SetMaxValue(float value) =>
        MaxValue = value;
}