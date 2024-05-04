using ProductionSystems.Logger;
using ProductionSystems.Options;
using ProductionSystems.Productions;
using static MathNet.Symbolics.Linq;

namespace ProductionSystems
{
    public class ProductionSystem
    {
        public IList<Fact> Facts { get; } = new List<Fact>();

        public IList<Rule> Rules { get; } = new List<Rule>();

        public ILogger? Logger { get; set; }

        public ProductionSystem(ILogger? logger = null)
        {
            Logger = logger;
        }

        public IEnumerable<Fact> Execute(ExecutionOptions? options = null)
        {
            if (options == null)
            {
                options = new ExecutionOptions();
            }
            var facts = Facts.ToList();
            var rules = Rules.ToList();
            var result = new List<Fact>();
            var removedRules = new List<Rule>();
            Setup(facts, result, rules, options);
            var isFindedRule = true;
            while (isFindedRule && IsNotTargetsAchieved(result, options.Targets))
            {
                foreach(var rule in removedRules)
                {
                    rules.Remove(rule);
                }
                removedRules.Clear();
                isFindedRule = false;
                foreach (var rule in rules)
                {
                    if(IsRemovedRule(rule, facts, options))
                    {
                        removedRules.Add(rule);
                    }
                    else
                    {
                        if (CheckRule(rule, facts, options))
                        {
                            Logger?.Log($"Successful rule: input: " +
                                $"{string.Join(", ", rule.InputFactNames)}; " +
                                $"output: {rule.OutputFactName}");
                            var newFact = rule.CalculateFact(facts, Logger);
                            facts.Add(newFact);
                            AddResultFact(newFact, result, options.Targets);
                            removedRules.Add(rule);
                            isFindedRule = true;
                            break;
                        }
                        else
                        {
                            if (!options.OutputMode.HasFlag(OutputMode.OnlySuccessfulSteps))
                            {
                                Logger?.Log($"Failed rule: input: " +
                                    $"{string.Join(", ", rule.InputFactNames)}; " +
                                    $"output: {rule.OutputFactName}");
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void Setup(List<Fact> facts, List<Fact> result,
            List<Rule> rules, ExecutionOptions options)
        {
            if (options.Targets != null)
            {
                result.AddRange(facts.Where((f) =>
                    options.Targets.Any((t) => t == f.Name)));
            }
            if (options.ExecutionMode.HasFlag
                (ExecutionMode.RuleWithLeastFactsSearch))
            {
                rules.Sort((r1, r2) => r1.InputFactNames.Count().
                    CompareTo(r2.InputFactNames.Count()));
            }
        }

        public bool IsNotTargetsAchieved(List<Fact> result,
            IEnumerable<string>? targets)
        {
            if(targets == null)
            {
                return true;
            }
            return result.Where((f) => targets.Any
                ((t) => t == f.Name)).Count() != targets.Count();
        }

        public void AddResultFact(Fact fact, List<Fact> result,
            IEnumerable<string>? targets)
        {
            if (targets == null || targets.Any((t) => t == fact.Name))
            {
                result.Add(fact);
            }
        }

        public bool CheckRule(Rule rule, IEnumerable<Fact> facts,
            ExecutionOptions options)
        {
            var result = rule.InputFactNames.All((n) => facts.Any((f) => f.Name == n));
            if (options.ExecutionMode.HasFlag(ExecutionMode.RemoveUselessRules))
            {
                return result;
            }
            return result && !facts.Any((f) => f.Name == rule.OutputFactName);
        }
            

        public bool IsRemovedRule(Rule rule, IEnumerable<Fact> facts,
            ExecutionOptions options) =>
            options.ExecutionMode.HasFlag(ExecutionMode.RemoveUselessRules) &&
            facts.Any((f) => f.Name == rule.OutputFactName);
    }
}
