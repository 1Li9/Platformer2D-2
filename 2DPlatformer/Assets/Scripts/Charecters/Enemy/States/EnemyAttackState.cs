public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy context) : base(context) { }

    public override void Update() =>
        Context.Attacker.Attack(Context.Timer, Context.AttackCooldownTime);

    public override void Exit() { }
}
