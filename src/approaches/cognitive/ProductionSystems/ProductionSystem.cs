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
            var findedRule = (Rule?)null;
            while (findedRule != null && IsNotFinal(result, options.Targets))
            {
                rules.Remove(findedRule);
                findedRule = null;
                foreach (var rule in rules)
                {
                    var newFact = rule.Check(facts, Logger);
                    if (newFact != null && facts.Where
                        ((f) => f.Name == newFact.Name).Count() == 0)
                    {
                        facts.Add(newFact);
                        findedRule = rule;
                        break;
                    }
                }
            }
            return result;
        }

        private void Setup(List<Fact> facts, List<Fact> result,
            List<Rule> rules, ExecutionOptions options)
        {
            if (options.Targets == null)
            {
                result.AddRange(facts);
            }
            else
            {
                result.AddRange(facts.Where((f) =>
                    options.Targets.Any((t) => t == f.Name)));
            }
            if (options.ExecutionMode ==
                ExecutionMode.RuleWithLeastFactsSearch)
            {
                rules.Sort((r1, r2) => r1.InputFactNames.Count().
                    CompareTo(r2.InputFactNames.Count()));
            }
        }

        public bool IsNotFinal(List<Fact> result,
            IEnumerable<string>? targets)
        {
            if(targets == null)
            {
                return true;
            }
            return result.Where((f) => targets.Any
                ((t) => t == f.Name)).Count() == targets.Count();
        }
    }
}
