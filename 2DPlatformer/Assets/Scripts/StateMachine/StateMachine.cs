using System.Collections.Generic;

public class StateMachine
{
    private readonly StatesPool _statesPool;

    private Parameters _parameters;
    private State _current;

    public StateMachine(Enemy context, StatesPool statesPool)
    {
        _statesPool = statesPool;
        _parameters = context.Parameters;
        _current = _statesPool.EntryState;
    }

    public void Update()
    {
        _current.Update();

        if (_current.NextStates is not List<State> nextStates || _current.NextStates.Count == 0)
            return;

        foreach (State nextState in nextStates)
        {
            var nextStateParameters = nextState.EnterConditions as List<Parameter>;

            if (IsParametersMatched(nextStateParameters))
            {
                _current.Exit();
                _current = nextState;

                return;
            }
        }
    }

    private bool IsParametersMatched(List<Parameter> parameters)
    {
        foreach (Parameter parameter in parameters)
            if (_parameters.Get(parameter.GetHashCode()).Value != parameter.Value)
                return false;

        return true;
    }
}
