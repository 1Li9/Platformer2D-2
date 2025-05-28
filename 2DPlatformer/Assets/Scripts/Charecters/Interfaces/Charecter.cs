using UnityEngine;
using System;

public abstract class Charecter : MonoBehaviour, IDamageble
{
    public abstract event Action<float> HealthChanged;
    public abstract event Action<float> MaxHealthChanged;

    public abstract Health Health { get; protected set; }

    public bool IsAlive => Health.IsAlive;

    public Transform Transform => transform;

    public event Action Dead;

    public bool TryTakeDamage(float damage)
    {
        if (Health.IsAlive == false)
            return false;

        Health.TakeDamage(damage);

        if (Health.IsAlive == false)
            Dead?.Invoke();

        return true;
    }
}
