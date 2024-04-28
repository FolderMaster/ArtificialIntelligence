namespace ProductionSystems.Productions
{
    public class Fact
    {
        public string Name { get; private set; }

        public object? Value { get; private set; }

        public Fact(string name)
        {
            Name = name;
            Value = null;
        }

        public Fact(string name, object? value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString() => $"Name = {Name}, Value = {Value}";
    }
}
