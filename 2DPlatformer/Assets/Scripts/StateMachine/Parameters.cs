using System.Collections.Generic;

public class Parameters
{
    private Dictionary<int, Parameter> _parameters;

    public Parameters(List<Parameter> parameters)
    {
        _parameters = new Dictionary<int, Parameter>();
        CreateParameters(parameters);
    }

    public Parameter Get(int hash)
    {
        if (_parameters.ContainsKey(hash))
            return _parameters[hash];

        throw new System.ArgumentNullException();
    }

    private void CreateParameters(List<Parameter> parameters)
    {
        foreach (Parameter parameter in parameters)
            _parameters.Add(parameter.GetHashCode(), parameter);
    }
}
