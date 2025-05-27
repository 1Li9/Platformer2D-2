using System;

public class Attacker : IAttacker
{
    public event Action Attacked;

    public void Attack(float damage, IAnimator animator, IDamageble damageble)
    {
        if (damageble?.TryTakeDamage(damage) == false)
            return;

        Attacked?.Invoke();
    }
}