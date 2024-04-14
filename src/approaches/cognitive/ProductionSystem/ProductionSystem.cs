using System.Data;

namespace ProductionSystem
{
    public class ProductionSystem
    {
        public IList<Fact> Facts { get; private set; } = new List<Fact>();

        public IList<Rule> Rules { get; private set; } = new List<Rule>();

        public Fact? Execute(string factName)
        {
            var facts = Facts.ToList();
            var rules = Rules.ToList();
            var flag = !facts.Select((f) => f.Name).Contains(factName);
            while (flag)
            {
                foreach (var rule in rules)
                {
                    var newFact = rule.Check(facts);
                    if (newFact != null && facts.Where((f) => f.Name == newFact.Name).Count() == 0)
                    {
                        rules.Remove(rule);
                        facts.Add(newFact);
                        if (newFact.Name == factName)
                        {
                            flag = true;
                            break;
                        }
                        flag = false;
                    }
                }
                flag = flag ? false : true;
            }
            return facts.Where((f) => f.Name == factName).FirstOrDefault();
        }

        public IEnumerable<Fact> Execute()
        {
            var facts = Facts.ToList();
            var rules = Rules.ToList();
            var newFacts = new List<Fact>();
            var flag = true;
            while (flag)
            {
                foreach (var rule in rules)
                {
                    rules.Remove(rule);
                    var newFact = rule.Check(facts);
                    if (newFact != null && facts.Where((f) => f.Name == newFact.Name).Count() == 0)
                    {
                        newFacts.Add(newFact);
                        facts.Add(newFact);
                        flag = false;
                    }
                }
                flag = flag ? false : true;
            }
            return newFacts;
        }
    }
}
