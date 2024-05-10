namespace NeuralNetworks.Trainers.Costs
{
    public class MSECost : ICost
    {
        public static readonly MSECost Current =
            new MSECost();

        public double Function
            (IEnumerable<double> prediction,
            IEnumerable<double> target)
        {
            var count = prediction.Count();
            var cost = 0d;
            for (var i = 0; i < count; ++i)
            {
                var error = prediction.ElementAt(i) -
                    target.ElementAt(i);
                cost += error * error;
            }
            return cost / 2;
        }

        public double Derivative(double prediction,
            double target) => prediction - target;
    }
}
