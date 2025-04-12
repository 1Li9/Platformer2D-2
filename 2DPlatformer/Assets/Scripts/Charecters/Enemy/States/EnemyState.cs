public abstract class EnemyState : State
{
    protected new Enemy Context;

    protected EnemyState(Enemy context) : base(context) =>
        Context = context;
}
