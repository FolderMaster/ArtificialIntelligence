namespace NeuralNetworks.Trainers.Costs
{
    public interface ICost
    {
        public double Function(IEnumerable<double> prediction, IEnumerable<double> target);

        public double Derivative(double prediction, double target);
    }
}
