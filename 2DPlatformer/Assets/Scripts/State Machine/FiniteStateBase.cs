using System;
using System.Collections.Generic;

public abstract class FiniteStateBase : IFiniteState
{
    private List<ITransition> _transitions;

    public FiniteStateBase() =>
        _transitions = new List<ITransition>();

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update(IStateChanger stateChanger)
    {
        if(TryGetNextState(out IFiniteState nextState))
            stateChanger.ChangeState(nextState);
    }

    public void AddTransition(ITransition transition)
    {
        if (transition == null)
            throw new NullReferenceException(nameof(transition));

        _transitions.Add(transition);
    }

    private bool TryGetNextState(out IFiniteState nextState)
    {
        nextState = null;

        foreach (var transition in _transitions)
        {
            if (transition.CanTransit)
            {
                nextState = transition.NextState;

                return true;
            }
        }

        return false;
    }
}
