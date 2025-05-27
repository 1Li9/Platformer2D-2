using System;

public class ActionChain
{
    private readonly Action[] _actions;

    public ActionChain(params Action[] actions) =>
        _actions = actions;

    public void InvokeAll()
    {
        foreach (Action action in _actions)
            action?.Invoke();
    }
}
