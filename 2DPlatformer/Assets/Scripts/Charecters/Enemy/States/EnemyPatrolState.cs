public class EnemyPatrolState : IState
{
    private const float ChangeTarget = .2f;

    public IState Update(Enemy context)
    {
        if (context.Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value)
            return new EnemyFollowState();

        TargetsMap targetsMap = context.TargetsMap;

        context.Follower.Follow(targetsMap.CurrentTarget, () => targetsMap.SelectNextTarget(), ChangeTarget);

        return this;
    }

    public void Exit(Enemy enemy) { }
}
