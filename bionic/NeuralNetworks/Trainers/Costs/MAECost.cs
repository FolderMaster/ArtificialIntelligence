namespace NeuralNetworks.Trainers.Costs
{
    public class MAECost : ICost
    {
        public static readonly MAECost Current =
            new MAECost();

        public double Function
            (IEnumerable<double> prediction,
            IEnumerable<double> target)
        {
            var count = prediction.Count();
            var cost = 0d;
            for (var i = 0; i < count; ++i)
            {
                cost += Math.Abs(prediction.ElementAt(i) -
                    target.ElementAt(i));
            }
            return cost / 2;
        }

        public double Derivative(double prediction,
            double target) => Math.Sign(prediction - target);
    }
}
