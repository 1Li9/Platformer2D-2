using System.Collections.Generic;

public abstract class State
{
    public IReadOnlyList<Parameter> EnterConditions { get; protected set; }
    public IReadOnlyList<State> NextStates { get; protected set; }

    public abstract void Update();
    public abstract void Exit();

    public void SetNextStates(List<State> states)
    {
        List<State> nextStates = new();

        foreach (State state in states)
            nextStates.Add(state);

        NextStates = nextStates;
    }
}