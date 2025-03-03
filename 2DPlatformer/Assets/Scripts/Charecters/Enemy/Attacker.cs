using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage;

    private IDamageble _damageble;
    private bool _canAttack = true;

    private Coroutine _timerAction;

    public event Action CanAttack;
    public event Action CanNotAttack;
    public event Action Attacked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageble damageble))
        {
            _damageble = damageble;
            CanAttack?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageble damageble))
        {
            _damageble = null;
            CanNotAttack?.Invoke();
        }
    }

    public void Attack(Timer timer, float cooldownTime)
    {
        if (_canAttack == false)
            return;

        _damageble?.TakeDamage(_damage);
        Attacked?.Invoke();
        SetCooldown(timer, cooldownTime);
        _canAttack = false;
    }

    private void SetCooldown(Timer timer, float cooldownTime)
    {
        if (_timerAction != null)
        {
            timer.StopCoroutine(_timerAction);
            _timerAction = null;
        }

        _timerAction = timer.DoActionDelayed(() => _canAttack = true, cooldownTime);
    }
}
