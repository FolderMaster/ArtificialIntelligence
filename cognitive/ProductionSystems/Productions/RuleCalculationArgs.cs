using ProductionSystems.Logger;

namespace ProductionSystems.Productions
{
    public class RuleCalculationArgs
    {
        public IDictionary<string, object?> Facts { get; set; }

        public ILogger? Logger { get; set; }

        public RuleCalculationArgs(IEnumerable<Fact> facts,
            ILogger? logger)
        {
            Facts = facts.ToDictionary((f) => f.Name, (f) => f.Value);
            Logger = logger;
        }
    }
}
