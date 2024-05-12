using SimMetrics.Net.Metric;
using SimMetrics.Net.API;

using CaseBasedReasoning.Cases;
using CaseBasedReasoning.Systems;

var ratingCaseBasedSystem = new RatingCaseBasedSystem(
    [
        new RatingCase("Headache", "Take painkillers.", 0.8),
        new RatingCase("Fever", "Rest and drink plenty of fluids.", 0.9),
        new RatingCase("Stomachache", "Avoid heavy meals and drink herbal tea.", 0.7),
        new RatingCase("Sore throat", "Gargle with warm salt water.", 0.85),
        new RatingCase("Sprained ankle", "Rest, ice, compress, elevate (RICE).", 0.75),
        new RatingCase("Common cold", "Get plenty of rest and stay hydrated.", 0.8)
    ]);

while (true)
{
    Console.WriteLine("Cases:");
    foreach (var @case in ratingCaseBasedSystem.Cases)
    {
        Console.WriteLine(@case);
    }

    Console.WriteLine("Write task:");
    var task = Console.ReadLine();

    var solution = ratingCaseBasedSystem.GetSolutionForTask(task);
    Console.WriteLine("Solution:");
    Console.WriteLine(solution);

    double rating = 0d;
    Console.WriteLine("Write rating:");
    if (!double.TryParse(Console.ReadLine(), out rating))
    {
        Console.WriteLine("Error!");
        break;
    }
    var newCase = new RatingCase(task, (string)solution, rating);
    ratingCaseBasedSystem.Retain(newCase);
}



public class RatingCase : ICase
{
    public object Task { get; private set; }

    public object Solution { get; private set; }

    public double Rating { get; private set; }

    public RatingCase(string task, string solution,
        double rating)
    {
        Task = task;
        Solution = solution;
        Rating = rating;
    }

    public override string ToString() =>
        $"Rating case ({Task}, {Solution}, {Rating})";
}

public class RatingCaseBasedSystem : BaseCaseBasedSystem
{
    private static readonly AbstractStringMetric _metric =
        new SmithWaterman();

    public double MinRating { get; set; } = 0.7;

    public double MinSimilarity { get; set; } = 0.7;

    public RatingCaseBasedSystem(IEnumerable<ICase> cases)
    {
        foreach(var @case in cases)
        {
            Retain(@case);
        }
    }

    protected override bool CheckCase(ICase @case) =>
        @case is RatingCase ratingCase && ratingCase.Task != null &&
        ratingCase.Solution != null && ratingCase.Rating > MinRating;

    protected override IEnumerable<ICase> Retrieve(object task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));
        var result = new List<ICase>();
        foreach (var @case in _cases)
        {
            if(@case is RatingCase ratingCase)
            {
                var similarity = _metric.GetSimilarity
                    (ratingCase.Task.ToString(), task.ToString());
                if(similarity > MinSimilarity)
                {
                    result.Add(ratingCase);
                }
            }
        }
        return result;
    }

    protected override object? Reuse(IEnumerable<ICase> cases)
    {
        if(!cases.Any())
        {
            return null;
        }
        return string.Join(" ", cases.Select((c) => c.Solution));
    }
}