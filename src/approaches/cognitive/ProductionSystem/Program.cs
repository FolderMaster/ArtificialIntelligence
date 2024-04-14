namespace ProductionSystem;

public class Program
{
    private static void Main(string[] args)
    {
        var productionSystem = new ProductionSystem();

        productionSystem.Rules.Add(new Rule(["V", "p"], "m", (fs) =>
        {
            var v = (double)fs.First((f) => f.Name == "V").Value;
            var p = (double)fs.First((f) => f.Name == "p").Value;
            return v * p;
        }));
        productionSystem.Rules.Add(new Rule([ "m", "p" ], "V", (fs) =>
        {
            var m = (double)fs.First((f) => f.Name == "m").Value;
            var p = (double)fs.First((f) => f.Name == "p").Value;
            return m / p;
        }));
        productionSystem.Rules.Add(new Rule(["m", "V"], "p", (fs) =>
        {
            var m = (double)fs.First((f) => f.Name == "m").Value;
            var V = (double)fs.First((f) => f.Name == "V").Value;
            return V / m;
        }));

        productionSystem.Facts.Add(new Fact("V", 100.0d));
        productionSystem.Facts.Add(new Fact("p", 3.0d));

        Console.WriteLine("Execution for fact m:");
        Console.WriteLine(productionSystem.Execute("m"));

        Console.WriteLine("Execution:");
        foreach (var fact in productionSystem.Execute())
        {
            Console.WriteLine(fact);
        }
    }
}