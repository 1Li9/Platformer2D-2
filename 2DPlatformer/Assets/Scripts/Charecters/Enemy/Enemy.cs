using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageble
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
    public Target PlayerTarget => _playerTarget;
    public Attacker Attacker => _attacker;
    public Follower Follower { get; private set; }
    public Parameters Parameters { get; private set; }
    public Collider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        _health = new Health(_beginHealthPoints);
        _flipper = new CharacterFlipper(this);
        Follower = new(this, _flipper, _moveSpeed);

        List<Parameter> parameters = new List<Parameter>()
        {
            new Parameter(nameof(ParametersData.Params.IsPlayerSpotted)),
            new Parameter(nameof(ParametersData.Params.CanAttack)),
            new Parameter(nameof(ParametersData.Params.IsDead))
        };
        Parameters = new Parameters(parameters);
        EnemyStatesPool enemyStatesPool = new(this);
        _stateMachine = new StateMachine(this, enemyStatesPool);

        _enemyAnimator = new EnemyAnimator(this, _animator);

        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() =>
        SubscribeActions();

    private void OnDisable() =>
        UnsubscribeActions();

    private void Update() =>
        _stateMachine.Update();

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
        {
            Parameters.Get(ParametersData.Params.IsDead).Value = true;
            Dead?.Invoke();
        }
    }

    private void SubscribeActions()
    {
        _attentionZone.PlayerSpotted += () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = true;
        _attentionZone.PlayerLost += () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = false;

        _attacker.BecameAbleToAttack += () => Parameters.Get(ParametersData.Params.CanAttack).Value = true;
        _attacker.BecameUnableToAttack += () => Parameters.Get(ParametersData.Params.CanAttack).Value = false;
    }

    private void UnsubscribeActions()
    {
        _attentionZone.PlayerSpotted -= () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = true;
        _attentionZone.PlayerLost -= () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = false;

        _attacker.BecameAbleToAttack -= () => Parameters.Get(ParametersData.Params.CanAttack).Value = true;
        _attacker.BecameUnableToAttack -= () => Parameters.Get(ParametersData.Params.CanAttack).Value = false;
    }
}
