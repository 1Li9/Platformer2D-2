public class Parameter
{
    public Parameter(string name)
    {
        Name = name;
        Value = false;
    }

    public string Name { get; }
    public bool Value { get; set; }

    public override int GetHashCode() =>
        Name.GetHashCode();
}
