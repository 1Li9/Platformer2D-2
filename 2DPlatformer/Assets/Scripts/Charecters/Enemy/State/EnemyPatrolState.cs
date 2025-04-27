public class EnemyPatrolState : EnemyStateBase
{
    private const float TargetEpsilonDistance = .2f;

    public EnemyPatrolState(Enemy context) : base(context) { }

    public override void Update(IStateChanger stateChanger)
    {
        base.Update(stateChanger);  

        TargetsMap targetsMap = Context.TargetsMap;

        Context.Follower.Follow(targetsMap.CurrentTarget, () => targetsMap.SelectNextTarget(), TargetEpsilonDistance);
    }
}
