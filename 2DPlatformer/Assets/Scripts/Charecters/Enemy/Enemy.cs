using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private Timer _timer;

    [SerializeField] private AttentionZone _zone;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private TargetsMap _map;
    [SerializeField] private Target _playerTarget;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackTime;
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _deathTime;

    private CharacterFlipper _flipper;
    private Health _health;

    private EnemyPatrolState _patrolState;
    private EnemyFollowState _followState;
    private EnemyAttackState _attackState;
    private EnemyDeathState _deathState;

    private IState _currentState;

    public event Action Dead;

    private void Awake()
    {
        _health = new Health(_beginHealthPoints);
        _flipper = new(this);
        Follower follower = new(this, _flipper, _speed);

        _patrolState = new EnemyPatrolState(follower, _map);
        _followState = new EnemyFollowState(follower, _playerTarget);
        _attackState = new EnemyAttackState(_attacker, _attackTime, _timer);
        _deathState = new EnemyDeathState(this, _timer, _deathTime);

        _currentState = _patrolState;
    }

    private void OnEnable() =>
        SubscribeActions();

    private void OnDisable() =>
        UnsubscribeActions();

    private void Update() =>
        _currentState.Update();

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
        {
            Dead?.Invoke();
            UnsubscribeActions();
            ChangeStateToDeath();
        }
    }

    private void ChangeStateToPatrol()
    {
        _currentState.Exit();
        _currentState = _patrolState;
    }

    private void ChangeStateToFollow()
    {
        _currentState.Exit();
        _currentState = _followState;
    }

    private void ChangeStateToAttack()
    {
        _currentState.Exit();
        _currentState = _attackState;
    }

    private void ChangeStateToDeath()
    {
        _currentState.Exit();
        _currentState = _deathState;
    }

    private void SubscribeActions()
    {
        _zone.PlayerSpotted += ChangeStateToFollow;
        _zone.PlayerLost += ChangeStateToPatrol;
        _attacker.CanAttack += ChangeStateToAttack;
        _attacker.CanNotAttack += ChangeStateToPatrol;
    }

    private void UnsubscribeActions()
    {
        _zone.PlayerSpotted -= ChangeStateToFollow;
        _zone.PlayerLost -= ChangeStateToPatrol;
        _attacker.CanAttack -= ChangeStateToAttack;
        _attacker.CanNotAttack -= ChangeStateToPatrol;
    }
}
