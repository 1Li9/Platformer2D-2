public class EnemyFollowState : IState
{
    public IState Update(Enemy context)
    {
        if (context.Parameters.Get(ParametersData.Params.IsPlayerSpotted).Value == false)
            return new EnemyPatrolState();
        else if(context.Parameters.Get(ParametersData.Params.CanAttack).Value)
            return new EnemyAttackState();

        IState nextState = this;

        context.Follower.Follow(context.PlayerTarget);

        return nextState;
    }

    public void Exit(Enemy enemy) { }
}
