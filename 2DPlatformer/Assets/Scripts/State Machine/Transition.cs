using System;
public class Transition : ITransition
{
    private Func<bool> _condition;

    public Transition(IFiniteState nextState, Func<bool> condition)
    {
        NextState= nextState;
        _condition= condition;
        
        if(_condition == null )
            throw new ArgumentNullException(nameof(_condition));
    }

    public IFiniteState NextState  { get; }
    public bool CanTransit => _condition.Invoke();
}