public class EnemyFollowState : EnemyStateBase
{
    public EnemyFollowState(Enemy context) : base(context) { }

    public override void Update(IStateChanger stateChanger)
    {
        base.Update(stateChanger);

        Context.Follower.Follow(Context.PlayerTarget);
    }
}
