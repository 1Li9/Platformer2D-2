using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;

    private Enemy _enemy;

    private void Awake() =>
        _enemy = GetComponent<Enemy>();

    private void OnEnable() =>
        SubscribeActions();

    private void OnDisable() =>
        UnsubscribeActions();

    private void SubscribeActions()
    {
        _attacker.Attacked += () => _animator.SetTrigger(EnemyAnimatorData.Params.IsAttacked);

        _enemy.Dead += () => _animator.SetTrigger(EnemyAnimatorData.Params.Dead);
    }

    private void UnsubscribeActions()
    {
        _attacker.Attacked -= () => _animator.SetTrigger(EnemyAnimatorData.Params.IsAttacked);

        _enemy.Dead -= () => _animator.SetTrigger(EnemyAnimatorData.Params.Dead);
    }
}
