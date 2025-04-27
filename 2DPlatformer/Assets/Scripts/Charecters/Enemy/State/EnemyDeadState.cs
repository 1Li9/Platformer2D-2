using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyStateBase
{
    private readonly float _deadTime = 2f;

    private Coroutine _timerAction;

    public EnemyDeadState(Enemy context) : base(context) { }

    public override void Enter()
    {
        Context.Rigidbody.velocity = Vector3.zero;
        Context.Rigidbody.isKinematic = true;
        Context.Collider.enabled = false;
        _timerAction = Context.Timer.DoActionDelayed(() => Exit(), _deadTime);
    }

    public override void Exit()
    {
        if (_timerAction == null)
            return;

        Context.StopCoroutine(_timerAction);
        _timerAction = null;
        Object.Destroy(Context.gameObject);
    }
}
