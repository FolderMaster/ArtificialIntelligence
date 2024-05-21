using ProductionSystems.Logger;

namespace ProductionSystems.Productions
{
    public class Rule
    {
        public IEnumerable<string> InputFactNames { get; private set; }

        public string OutputFactName { get; private set; }

        public Rule(IEnumerable<string> inputFactNames, string outputFactName)
        {
            InputFactNames = inputFactNames;
            OutputFactName = outputFactName;
        }

        protected virtual object? Calculation(RuleCalculationArgs args) => null;

        public Fact CalculateFact(IEnumerable<Fact> inputFacts, ILogger? logger = null) =>
            new Fact(OutputFactName, Calculation(new RuleCalculationArgs(inputFacts, logger)));

        public override string ToString() => 
            $"Rule(Output = {OutputFactName}; Inputs: {string.Join(", ", InputFactNames)})";
    }
}
