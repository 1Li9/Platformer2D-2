public class EnemyStatesPool : StatesPool
{
    private Enemy _context;

    private EnemyFollowState _follow;
    private EnemyPatrolState _patrol;
    private EnemyAttackState _attack;
    private EnemyDeadState _dead;

    public EnemyStatesPool(Enemy context)
    {
        _context = context;
        AnyState = new AnyState(context);

        CreateStates(context);
        CreateTransitions();
        EntryState = _patrol;
    }

    private void CreateStates(Enemy context)
    {
        _follow = new EnemyFollowState(context);
        _patrol = new EnemyPatrolState(context);
        _attack = new EnemyAttackState(context);
        _dead = new EnemyDeadState(context);
    }

    private void CreateTransitions()
    {
        _patrol.CreateTransition(() => _context.AttentionZone.IsPlayerSpotted, _follow);

        _follow.CreateTransition(() => _context.AttentionZone.IsPlayerSpotted == false, _follow);
        _follow.CreateTransition(() => _context.Attacker.CanAttack, _attack);

        _attack.CreateTransition(() => _context.Attacker.CanAttack == false, _follow);

        AnyState.CreateTransition(() => _context.HealthPoints <= 0f, _dead);
    }
}
