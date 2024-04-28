using ProductionSystems.Logger;

namespace ProductionSystems.Productions
{
    public class RuleCalculationArgs
    {
        public IEnumerable<Fact> Facts { get; set; }

        public ILogger? Logger { get; set; }

        public RuleCalculationArgs(IEnumerable<Fact> facts,
            ILogger? logger)
        {
            Facts = facts;
            Logger = logger;
        }

        public IDictionary<string, object?> GetFactsDictionary() =>
            Facts.ToDictionary((f) => f.Name, (f) => f.Value);
    }
}
