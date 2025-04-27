using System;
using System.Collections.Generic;

public class FiniteStateMachine : IFiniteStateMachine, IStateChanger
{
    private readonly Dictionary<string, IFiniteState> _states;
    private IFiniteState _currentState;

    public FiniteStateMachine() =>
        _states = new Dictionary<string, IFiniteState>();

    public void Update() =>
        _currentState.Update(this);

    public FiniteStateMachine AddState<T>(T state) where T : IFiniteState
    {
        _states[typeof(T).Name] = state;

        return this;
    }

    public FiniteStateMachine AddTransition<TFrom, TTo>(Func<bool> condition)
        where TFrom : IFiniteState
        where TTo : IFiniteState
    {

        string from = typeof(TFrom).Name;
        string to = typeof(TTo).Name;

        if (_states.TryGetValue(from, out IFiniteState fromState) == false)
            throw new ArgumentException();

        if (_states.TryGetValue(to, out IFiniteState toState) == false)
            throw new ArgumentException();

        fromState.AddTransition(new Transition(toState, condition));

        return this;
    }

    public FiniteStateMachine SetFirstState<T>() where T : IFiniteState
    {
       if(_states.TryGetValue(typeof(T).Name, out IFiniteState state) == false)
            throw new ArgumentException();

        _currentState = _states[typeof(T).Name];

        return this;
    }

    public void ChangeState(IFiniteState to)
    {
        _currentState.Exit();
        _currentState = to ?? throw new ArgumentNullException(nameof(to));
        _currentState.Enter();
    }
}
