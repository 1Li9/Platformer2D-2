using System;

public class Transition
{
    private Func<bool> _condtion;
    private State _nextState;

    public Transition(Func<bool> condtion, State nextState)
    {
        _condtion = condtion;
        _nextState = nextState;
    }

    public bool CanMoveToNext(out State next)
    {
        next = null;

        if (_condtion != null && _condtion.Invoke())
        {
            next = _nextState;

            return true;
        }

        return false;
    }
}
