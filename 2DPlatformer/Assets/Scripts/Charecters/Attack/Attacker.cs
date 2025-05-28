using System;

public class Attacker : IAttacker
{
    public event Action Attacked;

    public void Attack(float damage, IAnimator animator, IAttackZone zone)
    {
        IDamageble damageble = zone.Damageble;

        if (zone.CanAttack == false || damageble.TryTakeDamage(damage) == false)
            return;

        Attacked?.Invoke();
    }
}