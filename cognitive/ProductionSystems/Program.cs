using ProductionSystems.Logger;
using ProductionSystems.Options;
using ProductionSystems.Productions;
using ProductionSystems;

using MathNet.Symbolics;

var logger = new ConsoleLogger();
var productionSystem = new ProductionSystem(logger);

productionSystem.Rules.Add(new MathRule(["a", "b", "c"], "p", "a + b + c"));
productionSystem.Rules.Add(new MathRule(["p", "b", "c"], "a", "p - (b + c)"));
productionSystem.Rules.Add(new MathRule(["a", "p", "c"], "b", "p - (a + c)"));
productionSystem.Rules.Add(new MathRule(["a", "b", "p"], "c", "p - (a + b)"));

productionSystem.Rules.Add(new MathRule(["a", "alpha", "b"], "beta",
    "acos(b * sin(alpha) / a)"));
productionSystem.Rules.Add(new MathRule(["a", "alpha", "beta"], "b",
    "a * sin(beta) / sin(alpha)"));
productionSystem.Rules.Add(new MathRule(["c", "gamma", "b"], "beta",
    "acos(b * sin(gamma) / c)"));
productionSystem.Rules.Add(new MathRule(["c", "gamma", "beta"], "b",
    "c * sin(beta) / sin(gamma)"));

productionSystem.Rules.Add(new MathRule(["a", "alpha", "с"], "gamma",
    "acos(c * sin(alpha) / a)"));
productionSystem.Rules.Add(new MathRule(["a", "alpha", "gamma"], "c",
    "a * sin(gamma) / sin(alpha)"));
productionSystem.Rules.Add(new MathRule(["b", "beta", "c"], "gamma",
    "acos(c * sin(beta) / b)"));
productionSystem.Rules.Add(new MathRule(["b", "beta", "gamma"], "c",
    "b * sin(gamma) / sin(beta)"));

productionSystem.Rules.Add(new MathRule(["b", "beta", "a"], "alpha",
    "acos(a * sin(beta) / b)"));
productionSystem.Rules.Add(new MathRule(["b", "beta", "alpha"], "a",
    "b * sin(alpha) / sin(beta)"));
productionSystem.Rules.Add(new MathRule(["c", "gamma", "a"], "alpha",
    "acos(a * sin(gamma) / c)"));
productionSystem.Rules.Add(new MathRule(["c", "gamma", "alpha"], "a",
    "c * sin(alpha) / sin(gamma)"));

productionSystem.Rules.Add(new MathRule(["alpha", "beta"], "gamma",
    "pi - (alpha + beta)"));
productionSystem.Rules.Add(new MathRule(["alpha", "gamma"], "beta",
    "pi - (alpha + gamma)"));
productionSystem.Rules.Add(new MathRule(["gamma", "beta"], "alpha",
    "pi - (beta + gamma)"));

productionSystem.Rules.Add(new MathRule(["a", "b", "gamma"], "s",
    "1/2 * a * b * sin(gamma)"));
productionSystem.Rules.Add(new MathRule(["b", "c", "alpha"], "s",
    "1/2 * b * c * sin(alpha)"));
productionSystem.Rules.Add(new MathRule(["a", "c", "beta"], "s",
    "1/2 * a * c * sin(beta)"));
productionSystem.Rules.Add(new MathRule(["p", "a", "b", "c"], "s",
    "sqrt(p/2 * (p/2 - a) * (p/2 - b) * (p/2 - c))"));

productionSystem.Facts.Add(new Fact("a", (FloatingPoint)5d));
productionSystem.Facts.Add(new Fact("b", (FloatingPoint)12d));
productionSystem.Facts.Add(new Fact("beta", (FloatingPoint)0.9d));

Console.WriteLine("Primary facts:");
foreach (var value in productionSystem.Facts)
{
    Console.WriteLine(value);
}
Console.WriteLine("Execution:");
var values = productionSystem.Execute(new ExecutionOptions
    (ExecutionMode.RuleWithLeastFactsSearch | ExecutionMode.RemoveUselessRules,
    OutputMode.OnlySuccessfulSteps, null));
Console.WriteLine("Getted facts:");
foreach (var value in values)
{
    Console.WriteLine(value);
}

public class MathRule : Rule
{
    private string _expression;

    public MathRule(IEnumerable<string> inputFactNames,
        string outputFactName, string expression) :
        base(inputFactNames, outputFactName) => _expression = expression;

    protected override object? Calculation(RuleCalculationArgs args)
    {
        var expression = SymbolicExpression.Parse(_expression);
        var symbols = args.Facts.ToDictionary((f) => f.Key,
            (f) => (FloatingPoint)f.Value);
        var result = expression.Evaluate(symbols);
        args.Logger?.Log($"{OutputFactName}[{result}] = {expression}");
        return result;
    }
}
