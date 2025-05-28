using System;

public class HandAttacker : IAttacker
{
    private IAttacker _attacker;
    private float _cooldown;
    private Timer _timer;

    private bool _isCooldowned;

    public HandAttacker(IAttacker attacker, float cooldown, Timer timer)
    {
        _attacker = attacker;
        _cooldown = cooldown;
        _timer = timer;
    }

    public event Action Attacked;

    public void Attack(float damage, IAnimator animator, IAttackZone zone)
    {
        if (_isCooldowned)
            return;

        _attacker.Attack(damage, animator, zone);

        _isCooldowned = true;
        animator.UpdateIsAtackedTrigger();
        Attacked?.Invoke();
        _timer.DoActionDelayed(() => _isCooldowned = false, _cooldown);
    }
}