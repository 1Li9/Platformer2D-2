using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour, IDamageble//, IStateble
{
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private Timer _timer;
    [SerializeField] private Animator _animator;
    [SerializeField] private TargetsMap _targetsMap;
    [SerializeField] private AttentionZone _attentionZone;
    [SerializeField] private Target _playerTarget;
    [SerializeField] private Attacker _attacker;

    private EnemyAnimator _enemyAnimator;

    private Health _health;
    private CharacterFlipper _flipper;

    private FiniteStateMachine _stateMachine;

    public event Action Dead;

    public float AttackCooldownTime => _attackCooldownTime;
    public Timer Timer => _timer;
    public TargetsMap TargetsMap => _targetsMap;
    public AttentionZone AttentionZone => _attentionZone;
    public Target PlayerTarget => _playerTarget;
    public Attacker Attacker => _attacker;
    public float HealthPoints => _health.HealthPoints;
    public Follower Follower { get; private set; }
    public Collider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();

        _health = new Health(_enemyAnimator);
        _flipper = new CharacterFlipper(transform);
        Follower = new(this, _flipper, _moveSpeed);

        _stateMachine = new EnemyFiniteStateMachineFactory(this, _enemyAnimator).Create();

        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() =>
        _health.SetHealthPoints(_beginHealthPoints);

    private void Update() =>
        _stateMachine.Update();

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
        {
            Dead?.Invoke();
        }
    }
}
