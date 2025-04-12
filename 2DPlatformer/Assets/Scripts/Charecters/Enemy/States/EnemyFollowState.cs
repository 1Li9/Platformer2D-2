public class EnemyFollowState : EnemyState
{
    public EnemyFollowState(Enemy context) : base(context) { }

    public override void Update() =>
        Context.Follower.Follow(Context.PlayerTarget);

    public override void Exit() { }
}
