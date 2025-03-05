public class Parameter
{
    public Parameter(string name)
    {
        Name = name;
        Value = false;
    }

    public Parameter(string name, bool value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }
    public bool Value { get; set; }

    public override int GetHashCode() =>
        Name.GetHashCode();
}
