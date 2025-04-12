using System;
using System.Collections.Generic;

public abstract class State
{
    protected IStateble Context;

    private List<Transition> _transitions;

    public State(IStateble context)
    {
        Context = context;
        _transitions = new List<Transition>();
    }

    public abstract void Update();
    public abstract void Exit();

    public void CreateTransition(Func<bool> condition, State nextState)
    {
        Transition transition = new(condition, nextState);
        _transitions.Add(transition);
    }

    public bool CanMoveNext(out State next)
    {
        next = null;

        if (_transitions.Count == 0)
            return false;

        foreach (Transition transition in _transitions)
        {
            if (transition.CanMoveToNext(out next))
                return true;
        }

        return false;
    }
}