public interface ITransition
{
    public IFiniteState NextState { get; }
    public bool CanTransit { get; }
}