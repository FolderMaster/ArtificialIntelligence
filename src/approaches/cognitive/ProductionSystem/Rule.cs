namespace ProductionSystem
{
    public class Rule
    {
        public IEnumerable<string> InputFactNames { get; private set; }

        public string OutputFactName { get; private set; }

        public Func<IEnumerable<Fact>, object?> Calculation { get; private set; } =
            (fs) => null;

        public Rule(IEnumerable<string> inputFactNames, string outputFactName)
        {
            InputFactNames = inputFactNames;
            OutputFactName = outputFactName;
        }

        public Rule(IEnumerable<string> inputFactNames, string outputFactName,
            Func<IEnumerable<Fact>, object?> calculation) :
            this(inputFactNames, outputFactName) => Calculation = calculation;

        public Fact? Check(IEnumerable<Fact> inputFacts)
        {
            if (InputFactNames.OrderBy(n => n).SequenceEqual(inputFacts.Select(f => f.Name).OrderBy(n => n)))
            {
                return new Fact(OutputFactName, Calculation(inputFacts));
            }
            return null;
        }
    }
}
