using ProductionSystems.Logger;
using ProductionSystems.Options;
using ProductionSystems.Productions;

namespace ProductionSystems;

public class Program
{
    private static void Main(string[] args)
    {
        var logger = new ConsoleLogger();
        var productionSystem = new ProductionSystem()
        {
            Logger = logger
        };

        productionSystem.Rules.Add(new Rule(["a", "b", "c"], "p", (args) =>
        {
            var facts = args.GetFactsDictionary();
            var p = (double)facts["a"] + (double)facts["b"] +
                (double)facts["c"];
            args.Logger?.Log($"p[{p}] = a[{facts["a"]}] + " +
                $"b[{facts["b"]}] + c[{facts["c"]}]");
            return p;
        }));
        productionSystem.Rules.Add(new Rule(["p", "b", "c"], "a", (args) =>
        {
            var facts = args.GetFactsDictionary();
            var a = (double)facts["p"] - ((double)facts["b"] +
                (double)facts["c"]);
            args.Logger?.Log($"a[{a}] = p[{facts["p"]}] - " +
                $"(b[{facts["b"]}] + c[{facts["c"]}])");
            return a;
        }));
        productionSystem.Rules.Add(new Rule(["p", "a", "c"], "b", (args) =>
        {
            var facts = args.GetFactsDictionary();
            var b = (double)facts["p"] - ((double)facts["a"] +
                (double)facts["c"]);
            args.Logger?.Log($"b[{b}] = p[{facts["p"]}] - " +
                $"(a[{facts["a"]}] + c[{facts["c"]}])");
            return b;
        }));
        productionSystem.Rules.Add(new Rule(["p", "b", "c"], "c", (args) =>
        {
            var facts = args.GetFactsDictionary();
            var c = (double)facts["p"] - ((double)facts["a"] +
                (double)facts["b"]);
            args.Logger?.Log($"c[{c}] = p[{facts["p"]}] - " +
                $"(a[{facts["a"]}] + b[{facts["b"]}])");
            return c;
        }));

        productionSystem.Facts.Add(new Fact("a", 10.0d));
        productionSystem.Facts.Add(new Fact("b", 6.0d));
        productionSystem.Facts.Add(new Fact("c", 7.0d));

        Console.WriteLine("Execution:");
        productionSystem.Execute(new ExecutionOptions(ExecutionMode.RuleWithLeastFactsSearch,
            OutputMode.OutgoingOutput, OutputDataMode.WithUnsuccessfulSteps, null));
    }
}