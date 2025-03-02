public class EnemyPatrolState : IState
{
    private const float ChangeTargetDistance = .2f;

    private Follower _follower;
    private TargetsMap _map;

    public EnemyPatrolState(Follower follower, TargetsMap map)
    {
        _follower = follower;
        _map = map;
    }

    public void Update() =>
        _follower.Follow(_map.CurrentTarget, () => _map.SelectNextTarget(), ChangeTargetDistance);

    public void Exit() { }
}
