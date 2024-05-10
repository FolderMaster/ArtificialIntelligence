namespace NeuralNetworks.Trainers.Costs
{
    public class CrossEntropyCost : ICost
    {
        public const double e = 1e-10;

        public static readonly CrossEntropyCost Current =
            new CrossEntropyCost();

        public double Function(IEnumerable<double> prediction,
            IEnumerable<double> target)
        {
            var count = prediction.Count();
            var cost = 0d;
            for (var i = 0; i < count; ++i)
            {
                var p = prediction.ElementAt(i);
                var t = target.ElementAt(i);
                cost -= t * Math.Log(p > 0 ? p : e) + (1 - t) *
                    Math.Log(p < 1 ? 1 - p : e);
            }
            return cost / count;
        }

        public double Derivative(double prediction, double target) =>
            -target / (prediction > 0 && prediction < 1 ? prediction : e) +
            (1 - target) / (prediction > 0 && prediction < 1 ? 1 - prediction : e);
    }
}
