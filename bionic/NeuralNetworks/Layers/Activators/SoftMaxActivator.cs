namespace NeuralNetworks.Layers.Activators
{
    public class SoftMaxActivator : IActivator
    {
        public static readonly SoftMaxActivator
            Current = new SoftMaxActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            var calculatedValues = values.Select(Math.Exp);
            var sum = calculatedValues.Sum();
            foreach (var value in calculatedValues)
            {
                yield return value / sum;
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            var calculatedValues = values.Select(Math.Exp);
            var sum = calculatedValues.Sum();
            foreach (var value in calculatedValues)
            {
                yield return (value * sum - value * value) / (sum * sum);
            }
        }
    }
}
