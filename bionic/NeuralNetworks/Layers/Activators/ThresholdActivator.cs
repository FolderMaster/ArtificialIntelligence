namespace NeuralNetworks.Layers.Activators
{
    public class ThresholdActivator : IActivator
    {
        public static readonly ThresholdActivator
            Current = new ThresholdActivator();

        public double Derivative(double value) => 0;

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value >= 0 ? 1 : 0;
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return 0;
            }
        }
    }
}
