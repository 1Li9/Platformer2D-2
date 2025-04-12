using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageble, IStateble
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

    private Health _health;
    private CharacterFlipper _flipper;

    private StateMachine _stateMachine;
    private EnemyAnimator _enemyAnimator;

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
        _health = new Health(_beginHealthPoints);
        _flipper = new CharacterFlipper(this);
        Follower = new(this, _flipper, _moveSpeed);

        EnemyStatesPool enemyStatesPool = new(this);
        _stateMachine = new StateMachine(enemyStatesPool);

        _enemyAnimator = new EnemyAnimator(this, _animator);

        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

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
