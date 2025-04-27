public class EnemyFiniteStateMachineFactory
{
    private Enemy _context;
    private IAnimator _animator;

    public EnemyFiniteStateMachineFactory(Enemy context, IAnimator animator)
    {
        _context = context;
        _animator = animator;
    }

    public FiniteStateMachine Create()
    {
        FiniteStateMachine finiteStateMachine = new();

        finiteStateMachine
            .AddState(new EnemyFollowState(_context))
            .AddState(new EnemyPatrolState(_context))
            .AddState(new EnemyAttackState(_context, _animator))
            .AddState(new EnemyDeadState(_context))
            .AddTransition<EnemyPatrolState, EnemyFollowState>(() => _context.AttentionZone.IsPlayerSpotted)
            .AddTransition<EnemyFollowState, EnemyPatrolState>(() => _context.AttentionZone.IsPlayerSpotted == false)
            .AddTransition<EnemyFollowState, EnemyAttackState>(() => _context.Attacker.CanAttack)
            .AddTransition<EnemyAttackState, EnemyFollowState>(() => _context.Attacker.CanAttack == false)
            .AddTransition<EnemyPatrolState, EnemyDeadState>(() => _context.HealthPoints <= 0)
            .AddTransition<EnemyAttackState, EnemyDeadState>(() => _context.HealthPoints <= 0)
            .AddTransition<EnemyFollowState, EnemyDeadState>(() => _context.HealthPoints <= 0)
            .SetFirstState<EnemyPatrolState>();

        return finiteStateMachine;
    }
}
