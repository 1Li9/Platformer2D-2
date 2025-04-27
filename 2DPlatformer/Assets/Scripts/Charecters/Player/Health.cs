using System;

public class Health
{
    private IAnimator _animator;

    public Health(IAnimator animator) =>
        _animator = animator;


    public event Action<float> OnHealthPointsChanged;

    public float HealthPoints { get; private set; }
    public bool IsAlive { get; private set; } = true;

    public void SetHealthPoints(float health)
    {
        HealthPoints = health;
        OnHealthPointsChanged?.Invoke(HealthPoints);
    }

    public void TakeDamage(float damage)
    {
        HealthPoints -= damage;

        OnHealthPointsChanged?.Invoke(HealthPoints);

        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            IsAlive = false;
            _animator.UpdateDeadTrigger();
        }
    }

    public void Heal(float health)
    {
        if (IsAlive == false)
            return;

        OnHealthPointsChanged?.Invoke(HealthPoints);

        HealthPoints += health;
    }
}
