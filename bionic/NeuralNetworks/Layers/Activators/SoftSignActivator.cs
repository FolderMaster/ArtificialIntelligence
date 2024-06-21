namespace NeuralNetworks.Layers.Activators
{
    public class SoftSignActivator : IActivator
    {
        public static readonly SoftSignActivator
            Current = new SoftSignActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value / (1 + Math.Abs(value));
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                var d = 1 + Math.Abs(value);
                yield return 1 / (d * d);
            }
        }
    }
}
