public class StateMachine
{
    private readonly StatesPool _statesPool;
    private readonly State _any;

    private State _current;

    public StateMachine(StatesPool statesPool)
    {
        _statesPool = statesPool;
        _any = _statesPool.AnyState;
        _current = _statesPool.EntryState;
    }

    public void Update()
    {
        _current.Update();

        if(_any.CanMoveNext(out State next) || _current.CanMoveNext(out next))
            _current = next;
    }
}
