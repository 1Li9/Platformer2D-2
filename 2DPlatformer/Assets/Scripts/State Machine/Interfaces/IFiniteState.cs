public interface IFiniteState
{
    public void Enter();

    public void Exit();

    public void Update(IStateChanger stateChanger);

    public void AddTransition(ITransition transition);
}