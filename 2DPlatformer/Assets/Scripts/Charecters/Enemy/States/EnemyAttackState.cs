using UnityEngine;

public class EnemyAttackState : IState
{
    private Attacker _attacker;
    private Timer _timer;
    private Coroutine _timerCoroutine;
    private float _attackTime;

    public EnemyAttackState(Attacker attacker, float attackTime, Timer timer)
    {
        _attacker = attacker;
        _attackTime = attackTime;
        _timer = timer;
    }

    public void Update()
    {
        if (_timerCoroutine != null)
            return;

        _timerCoroutine = _timer.DoActionRepeating(() => _attacker.Attack(), _attackTime);
    }

    public void Exit()
    {
        if (_timerCoroutine == null | _timer == null)
            return;

        _timer.StopCoroutine(_timerCoroutine);
        _timerCoroutine = null;
    }
}
