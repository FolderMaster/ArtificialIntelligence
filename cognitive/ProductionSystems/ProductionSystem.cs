using ProductionSystems.Logger;
using ProductionSystems.Options;
using ProductionSystems.Productions;

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
            Setup(facts, result, rules, options);
            if (options.ExecutionMode.HasFlag
                (ExecutionMode.BackwardChaining))
            {
                ExecuteBackwardChaining(facts, result, rules, options);
            }
            else
            {
                ExecuteForwardChaining(facts, result, rules, options);
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
                if (options.OutputMode.HasFlag(OutputMode.WithAdditionalOutput))
                {
                    Logger?.Log("Founded facts:");
                    foreach (var fact in result)
                    {
                        Logger?.Log(fact);
                    }
                }
            }
            if (options.ExecutionMode.HasFlag
                (ExecutionMode.RuleWithLeastFactsSearch))
            {
                rules.Sort((r1, r2) => r1.InputFactNames.Count().
                    CompareTo(r2.InputFactNames.Count()));
            }
        }

        private void ExecuteBackwardChaining(List<Fact> facts,
            List<Fact> result, List<Rule> rules, ExecutionOptions options)
        {
            var neededRules = new Stack<Rule>();
            if(options.Targets != null)
            {
                var targets = new Stack<string>();
                foreach (var target in options.Targets.Where
                    ((t) => !result.Any((r) => r.Name == t)))
                {
                    targets.Push(target);
                }
                var removedRules = new List<Rule>();
                while (targets.Any())
                {
                    foreach (var rule in removedRules)
                    {
                        rules.Remove(rule);
                    }
                    removedRules.Clear();
                    var target = targets.Pop();
                    var foundedRules = 
                        GetRulesByOutputFactName(rules, facts, target);
                    foreach (var rule in foundedRules)
                    {
                        removedRules.Add(rule);
                        neededRules.Push(rule);
                        foreach(var inputFactName in rule.InputFactNames)
                        {
                            if(IsTargetFactName
                                (facts, targets, inputFactName))
                            {
                                targets.Push(inputFactName);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var rule in rules.
                    Where((r) => !result.Any
                    ((t) => t.Name == r.OutputFactName)))
                {
                    neededRules.Push(rule);
                }
            }
            ExecuteForwardChaining(facts, result,
                neededRules.ToList(), options);
        }

        private IEnumerable<Rule> GetRulesByOutputFactName
            (List<Rule> rules, List<Fact> facts, string outputFactName) =>
            rules.Where((r) => r.OutputFactName == outputFactName &&
            !facts.Any((r) => r.Name == outputFactName));

        private bool IsTargetFactName(List<Fact> facts,
            IEnumerable<string> targets, string target) =>
            !targets.Contains(target) && !facts.Any((r) => r.Name == target);

        private void ExecuteForwardChaining(List<Fact> facts,
            List<Fact> result, List<Rule> rules, ExecutionOptions options)
        {
            var isFoundedRule = true;
            var removedRules = new List<Rule>();
            if (options.OutputMode.HasFlag(OutputMode.WithAdditionalOutput))
            {
                Logger?.Log($"Founded rules:");
                foreach(var rule in rules)
                {
                    Logger?.Log(rule);
                }
                Logger?.Log($"Search facts:");
            }
            while (isFoundedRule && IsNotTargetsAchieved(result, options.Targets))
            {
                foreach (var rule in removedRules)
                {
                    rules.Remove(rule);
                }
                removedRules.Clear();
                isFoundedRule = false;
                foreach (var rule in rules)
                {
                    if (IsRemovedRule(rule, facts, options))
                    {
                        removedRules.Add(rule);
                        if(options.OutputMode.HasFlag(OutputMode.WithAdditionalOutput))
                        {
                            Logger?.Log($"Remove {rule}");
                        }
                    }
                    else
                    {
                        if (CheckForwardRule(rule, facts, options))
                        {
                            Logger?.Log($"Success {rule}");
                            var newFact = rule.CalculateFact(facts, Logger);
                            facts.Add(newFact);
                            AddResultFact(newFact, result, options.Targets);
                            removedRules.Add(rule);
                            isFoundedRule = true;
                            break;
                        }
                        else
                        {
                            if (!options.OutputMode.HasFlag
                                (OutputMode.OnlySuccessfulSteps))
                            {
                                Logger?.Log($"Fail {rule}");
                            }
                        }
                    }
                }
            }
        }

        private bool IsNotTargetsAchieved(List<Fact> result,
            IEnumerable<string>? targets)
        {
            if(targets == null)
            {
                return true;
            }
            return result.Where((f) => targets.Any
                ((t) => t == f.Name)).Count() != targets.Count();
        }

        private void AddResultFact(Fact fact, List<Fact> result,
            IEnumerable<string>? targets)
        {
            if (targets == null || targets.Any((t) => t == fact.Name))
            {
                result.Add(fact);
            }
        }

        private bool CheckForwardRule(Rule rule, IEnumerable<Fact> facts,
            ExecutionOptions options)
        {
            var result = rule.InputFactNames.All((n) => facts.Any((f) => f.Name == n));
            if (options.ExecutionMode.HasFlag(ExecutionMode.RemoveUselessRules))
            {
                return result;
            }
            return result && !facts.Any((f) => f.Name == rule.OutputFactName);
        }

        private bool IsRemovedRule(Rule rule, IEnumerable<Fact> facts,
            ExecutionOptions options) =>
            options.ExecutionMode.HasFlag(ExecutionMode.RemoveUselessRules) &&
            facts.Any((f) => f.Name == rule.OutputFactName);
    }
}
