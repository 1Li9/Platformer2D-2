using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage;

    private IDamageble _damageble;
    private Coroutine _timerAction;
    private bool _isAttackCooldowned = false;

    public event Action Attacked;

    public bool CanAttack { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageble damageble))
        {
            _damageble = damageble;
            CanAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageble damageble))
        {
            _damageble = null;
            CanAttack = false;
        }
    }

    public void Attack(Timer timer, float cooldownTime, IAnimator animator)
    {
        if (_isAttackCooldowned)
            return;

        _damageble?.TakeDamage(_damage);
        Attacked?.Invoke();
        animator.UpdateIsAtackedTrigger();
        SetCooldown(timer, cooldownTime);
        _isAttackCooldowned = true;
    }

    private void SetCooldown(Timer timer, float cooldownTime)
    {
        if (_timerAction != null)
        {
            timer.StopCoroutine(_timerAction);
            _timerAction = null;
        }

        _timerAction = timer.DoActionDelayed(() => _isAttackCooldowned = false, cooldownTime);
    }
}
