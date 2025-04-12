public class EnemyPatrolState : EnemyState
{
    private const float ChangeTarget = .2f;

    public EnemyPatrolState(Enemy context) : base(context) { }

    public override void Update()
    {
        TargetsMap targetsMap = Context.TargetsMap;

        Context.Follower.Follow(targetsMap.CurrentTarget, () => targetsMap.SelectNextTarget(), ChangeTarget);
    }

    public override void Exit() { }
}
