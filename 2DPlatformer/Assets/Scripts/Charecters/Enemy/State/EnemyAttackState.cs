public class EnemyAttackState : EnemyStateBase
{
    private IAnimator _animator;

    public EnemyAttackState(Enemy context, IAnimator animator) : base(context) =>
        _animator = animator;

    public override void Update(IStateChanger stateChanger)
    {
        base.Update(stateChanger);

        Context.Attacker.Attack(Context.Timer, Context.AttackCooldownTime, _animator);
    }
}
