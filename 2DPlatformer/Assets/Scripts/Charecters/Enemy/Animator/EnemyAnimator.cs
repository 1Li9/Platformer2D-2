using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy= GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _attacker.Attacked += UpdateTriggerIsAtacked;
        _enemy.Dead+= UpdateDead;
    }

    private void OnDisable()
    {
        _attacker.Attacked -= UpdateTriggerIsAtacked;
        _enemy.Dead += UpdateDead;
    }

    private void UpdateTriggerIsAtacked() =>
        _animator.SetTrigger(EnemyAnimatorData.Params.IsAttacked);

    private void UpdateDead() =>
        _animator.SetTrigger(EnemyAnimatorData.Params.Dead);
}
