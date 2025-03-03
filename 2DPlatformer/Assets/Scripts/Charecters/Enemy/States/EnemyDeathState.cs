using UnityEngine;
public class EnemyDeathState : IState
{
    private readonly float _deadTime = 2f;

    private Coroutine _timerAction;

    public IState Update(Enemy context)
    {
        if (_timerAction != null)
            return this;

        context.Rigidbody.isKinematic = true;
        context.Collider.enabled = false;
        _timerAction = context.Timer.DoActionDelayed(() => Exit(context), _deadTime);

        return this;
    }

    public void Exit(Enemy context)
    {
        context.StopCoroutine(_timerAction);
        _timerAction = null;

        Object.Destroy(context.gameObject);
    }
}
