using System;

public class VampirismAttacker : IAttacker
{
    private readonly float _reloadTime;
    private readonly float _attackTime;
    private readonly float _coolDown;
    private readonly float _healPoints;

    private IAttacker _attacker;
    private IHealeble _healeble;
    private Timer _timer;

    private bool _isReloading;
    private bool _isAttacking;

    public VampirismAttacker(IAttacker attacker, IHealeble healeble, float attackTime, float reloadTime, float healPoints, Timer timer)
    {
        _reloadTime = reloadTime;
        _attackTime = attackTime;
        _coolDown = 1f;
        _healPoints = healPoints;
        _attacker = attacker;
        _healeble = healeble;
        _timer = timer;
    }

    public event Action Attacked;
    public event Action<float> Reloading;

    public void Attack(float damage, IAnimator animator, IAttackZone zone)
    {
        if (_isReloading | _isAttacking)
            return;

        float currentProgression = _attackTime;
        _isReloading = true;

        Attacked?.Invoke();

        _timer.DoActionWhileDelayed(
            action: () => Attack(damage, animator, zone, ref currentProgression),
            condition: () => currentProgression > 0f,
            callback: () => ChangeConditions(currentProgression),
            delayTime: _coolDown);
    }

    private void Attack(float damage, IAnimator animator, IAttackZone zone, ref float currentTime)
    {
        if (zone.CanAttack)
        {
            _attacker.Attack(damage, animator, zone);
            _healeble.IncreaseHealth(_healPoints);
        }

        currentTime -= _coolDown;
    }

    private void ChangeConditions(float beginValue)
    {
        beginValue /= _attackTime;
        _isAttacking = false;
        _timer.DoActionDelayed(() => _isReloading = false, _reloadTime);
        Reloading?.Invoke(beginValue);
    }
}