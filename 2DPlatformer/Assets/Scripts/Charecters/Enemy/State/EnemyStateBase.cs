public class EnemyStateBase : FiniteStateBase
{
    protected readonly Enemy Context;

    public EnemyStateBase(Enemy context)
    {
        Context = context;
    }
}
