using System.Collections.Generic;

public class EnemyStatesPool : StatesPool
{
    public EnemyStatesPool(Enemy context)
    {
        Follow = new EnemyFollowState(context);
        Patrol = new EnemyPatrolState(context);
        Attack = new EnemyAttackState(context);
        Dead = new EnemyDeadState(context);

        CreateTransitions();

        EntryState = Patrol;
    }

    public EnemyFollowState Follow { get; }
    public EnemyPatrolState Patrol { get; }
    public EnemyAttackState Attack { get; }
    public EnemyDeadState Dead { get; }

    private void CreateTransitions()
    {
        Follow.SetNextStates(new List<State> { Dead, Patrol, Attack });
        Patrol.SetNextStates(new List<State> { Dead, Follow });
        Attack.SetNextStates(new List<State> { Dead, Follow });
    }
}
