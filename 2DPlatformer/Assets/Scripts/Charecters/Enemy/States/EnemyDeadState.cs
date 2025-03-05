using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : State
{
    private readonly float _deadTime = 2f;

    private Coroutine _timerAction;

    private Enemy _context;

    public EnemyDeadState(Enemy context)
    {
        _context = context;

        EnterConditions = new List<Parameter>()
        {
            new Parameter(nameof(ParametersData.Params.IsDead), true)
        };
    }

    public override void Update()
    {
        if (_timerAction != null)
            return;

        _context.Rigidbody.velocity = Vector3.zero;
        _context.Rigidbody.isKinematic = true;
        _context.Collider.enabled = false;
        _timerAction = _context.Timer.DoActionDelayed(() => Exit(), _deadTime);
    }

    public override void Exit()
    {
        if (_timerAction == null)
            return;

        _context.StopCoroutine(_timerAction);
        _timerAction = null;
        Object.Destroy(_context.gameObject);
    }
}
