using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    [SerializeField] private PatrolZone _zone;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private TargetsMap _map;
    [SerializeField] private Target _playerTarget;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackTime;

    private CharacterFlipper _flipper;

    private IState _currentState;
    private EnemyPatrolState _patrolState;
    private EnemyFollowState _followState;
    private EnemyAttackState _attackState;

    private void Awake()
    {
        _flipper = new(this);
        Follower follower = new(this, _flipper, _speed);

        _patrolState = new EnemyPatrolState(follower, _map);
        _followState = new EnemyFollowState(follower, _playerTarget);
        _attackState = new EnemyAttackState(_attacker, _attackTime, _timer);

        _currentState = _patrolState;
    }

    private void OnEnable()
    {
        _zone.PlayerSpotted += ChangeStateToFollow;
        _zone.PlayerLost += ChangeStateToPatrol;
        _attacker.CanAttack += ChangeStateToAttack;
    }

    private void OnDisable()
    {
        _zone.PlayerSpotted -= ChangeStateToFollow;
        _zone.PlayerLost -= ChangeStateToPatrol;
        _attacker.CanAttack -= ChangeStateToAttack;
    }

    private void Update()
    {
        _currentState.Update();
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
}
