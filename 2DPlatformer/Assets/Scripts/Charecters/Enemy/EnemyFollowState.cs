using System;

public class EnemyFollowState : IState
{
    private const float AttackDistance = 1f;

    private Follower _follower;
    private Target _playerTarget;
    private Attacker _attacker;

    public EnemyFollowState(Follower follower, Target playerTarger)
    {
        _follower = follower;
        _playerTarget = playerTarger;
    }

    public void Update() =>
        _follower.Follow(_playerTarget, AttackDistance);

    public void Exit() { }
}
