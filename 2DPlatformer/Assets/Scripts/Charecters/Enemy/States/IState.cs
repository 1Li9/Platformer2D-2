public interface IState
{
    public IState Update(Enemy context);
    public void Exit(Enemy enemy);
}
