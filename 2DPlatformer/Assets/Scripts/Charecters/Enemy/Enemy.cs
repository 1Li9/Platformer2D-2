using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private Timer _timer;

    [SerializeField] private PatrolZone _zone;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private TargetsMap _map;
    [SerializeField] private Target _playerTarget;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackTime;
    [SerializeField] private float _beginHealthPoints;

    private CharacterFlipper _flipper;
    private Health _health;

    private EnemyPatrolState _patrolState;
    private EnemyFollowState _followState;
    private EnemyAttackState _attackState;

    public event Action Dead;
    public IState CurrentState { get; private set; }

    private void Awake()
    {
        _health = new Health(_beginHealthPoints);
        _flipper = new(this);
        Follower follower = new(this, _flipper, _speed);

        _patrolState = new EnemyPatrolState(follower, _map);
        _followState = new EnemyFollowState(follower, _playerTarget);
        _attackState = new EnemyAttackState(_attacker, _attackTime, _timer);

        CurrentState = _patrolState;
    }

    private void OnEnable() =>
        SubscribeActions();

    private void OnDisable() =>
        UnsubscribeActions();

    private void Update()
    {
        if(_health.IsAlive)
            CurrentState.Update();
    }

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
        {
            Dead?.Invoke();
            UnsubscribeActions();
            ChangeStateToPatrol();
        }
    }

    private void ChangeStateToPatrol()
    {
        CurrentState.Exit();
        CurrentState = _patrolState;
    }

    private void ChangeStateToFollow()
    {
        CurrentState.Exit();
        CurrentState = _followState;
    }

    private void ChangeStateToAttack()
    {
        CurrentState.Exit();
        CurrentState = _attackState;
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
