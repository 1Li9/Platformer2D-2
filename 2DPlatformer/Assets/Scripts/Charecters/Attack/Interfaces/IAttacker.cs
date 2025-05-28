using System;

public interface IAttacker
{
    public event Action Attacked;

    public abstract void Attack(float damage, IAnimator animator, IAttackZone zone);
}