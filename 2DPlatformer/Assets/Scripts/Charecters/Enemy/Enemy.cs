using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private Timer _timer;
    [SerializeField] private TargetsMap _targetsMap;
    [SerializeField] private AttentionZone _attentionZone;
    [SerializeField] private Target _playerTarget;
    [SerializeField] private Attacker _attacker;

    private Health _health;
    private CharacterFlipper _flipper;

    private IState _currentState;
    private EnemyDeathState _deathState;

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
        _deathState = new EnemyDeathState();

        Follower = new(this, _flipper, _moveSpeed);

        List<Parameter> parameters = new List<Parameter>()
        {
            new Parameter(nameof(ParametersData.Params.IsPlayerSpotted)),
            new Parameter(nameof(ParametersData.Params.CanAttack))
        };

        Parameters = new Parameters(parameters);
        _currentState = new EnemyPatrolState();

        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() =>
        SubscribeActions();

    private void OnDisable() =>
        UnsubscribeActions();

    private void Update() =>
        _currentState = _currentState.Update(this);

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
        {
            _currentState.Exit(this);
            _currentState = _deathState;
            Dead?.Invoke();
        }
    }

    private void SubscribeActions()
    {
        _attentionZone.PlayerSpotted += () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = true;
        _attentionZone.PlayerLost += () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = false;

        _attacker.CanAttack += () => Parameters.Get(ParametersData.Params.CanAttack).Value = true;
        _attacker.CanNotAttack += () => Parameters.Get(ParametersData.Params.CanAttack).Value = false;
    }

    private void UnsubscribeActions()
    {
        _attentionZone.PlayerSpotted -= () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = true;
        _attentionZone.PlayerLost -= () => Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value = false;

        _attacker.CanAttack -= () => Parameters.Get(ParametersData.Params.CanAttack).Value = true;
        _attacker.CanNotAttack -= () => Parameters.Get(ParametersData.Params.CanAttack).Value = false;
    }
}
