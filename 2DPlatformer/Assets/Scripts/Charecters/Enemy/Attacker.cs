using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage;

    private IDamageble _damageble;

    public event Action CanAttack;

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
            _damageble = null;
    }

    public void Attack() =>
        _damageble?.TakeDamage(_damage);
}
