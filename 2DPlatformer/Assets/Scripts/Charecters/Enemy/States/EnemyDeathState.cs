using UnityEngine;

public class EnemyDeathState : IState
{
    private MonoBehaviour _context;
    private Timer _timer;
    private float _deathTime;
    private Coroutine _timerCoroutine;

    public EnemyDeathState(MonoBehaviour context, Timer timer, float deathTime)
    {
        _context = context;
        _timer = timer;
        _deathTime = deathTime;
    }

    public void Update()
    {
        if (_timerCoroutine != null)
            return;

        _timerCoroutine = _timer.DoActionDelayed(() => Object.Destroy(_context.gameObject), _deathTime);
    }

    public void Exit()
    {
        if (_timerCoroutine == null | _timer == null)
            return;

        _timer.StopCoroutine(_timerCoroutine);
        _timerCoroutine = null;
    }
}
