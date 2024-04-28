using ProductionSystems.Logger;

namespace ProductionSystems.Productions
{
    public class Rule
    {
        public delegate object? CalculationAction(RuleCalculationArgs args);

        public IEnumerable<string> InputFactNames { get; private set; }

        public string OutputFactName { get; private set; }

        public CalculationAction Calculation { get; private set; } = (fs) => null;

        public Rule(IEnumerable<string> inputFactNames, string outputFactName)
        {
            InputFactNames = inputFactNames;
            OutputFactName = outputFactName;
        }

        public Rule(IEnumerable<string> inputFactNames, string outputFactName,
            CalculationAction calculation) :
            this(inputFactNames, outputFactName) => Calculation = calculation;

        public Fact? Check(IEnumerable<Fact> inputFacts, ILogger? logger = null)
        {
            if (InputFactNames.OrderBy(n => n).
                SequenceEqual(inputFacts.Select(f => f.Name).OrderBy(n => n)))
            {
                return new Fact(OutputFactName, Calculation.Invoke
                    (new RuleCalculationArgs(inputFacts, logger)));
            }
            return null;
        }
    }
}
