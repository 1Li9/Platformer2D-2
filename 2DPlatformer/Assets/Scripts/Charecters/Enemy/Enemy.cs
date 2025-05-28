using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(EnemyAnimator))]
public class Enemy : Charecter, IDamageble
{
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private Timer _timer;
    [SerializeField] private Animator _animator;
    [SerializeField] private TargetsMap _targetsMap;
    [SerializeField] private AttentionZone _attentionZone;
    [SerializeField] private Target _playerTarget;
    [SerializeField] private HandAttacker _attacker;
    [SerializeField] private View _view;
    [SerializeField] private HandAttackZone _attackTrigger;


    private EnemyAnimator _enemyAnimator;
    private CharacterFlipper _flipper;
    private FiniteStateMachine _stateMachine;

    public override event Action<float> HealthChanged;
    public override event Action<float> MaxHealthChanged;

    public float AttackCooldownTime => _attackCooldownTime;
    public float Damage => _damage;
    public Timer Timer => _timer;
    public TargetsMap TargetsMap => _targetsMap;
    public AttentionZone AttentionZone => _attentionZone;
    public Target PlayerTarget => _playerTarget;
    public HandAttacker Attacker => _attacker;
    public HandAttackZone AttackTrigger => _attackTrigger;
    public EnemyAnimator EnemyAnimator => _enemyAnimator;
    public Follower Follower { get; private set; }
    public Collider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public override Health Health { get; protected set; }

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();

        Health = new Health(_enemyAnimator, _beginHealthPoints);
        _flipper = new CharacterFlipper(_view.transform);
        Follower = new(this, _flipper, _moveSpeed);

        _stateMachine = new EnemyFiniteStateMachineFactory(this, _enemyAnimator).Create();

        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Attacker attacker = new();
        _attacker = new HandAttacker(attacker, AttackCooldownTime, Timer);
    }

    private void OnEnable()
    {
        Health.ValueChanged += (value) => HealthChanged?.Invoke(value);
        Health.MaxValueChanged += (value) => MaxHealthChanged?.Invoke(value);
    }

    private void OnDisable()
    {
        Health.ValueChanged -= (value) => HealthChanged?.Invoke(value);
        Health.MaxValueChanged -= (value) => MaxHealthChanged?.Invoke(value);
    }

    private void Start() =>
        Health.Initialize();

    private void Update() =>
        _stateMachine.Update();
}