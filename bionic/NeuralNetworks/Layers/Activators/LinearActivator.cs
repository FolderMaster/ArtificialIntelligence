namespace NeuralNetworks.Layers.Activators
{
    internal class LinearActivator : IActivator
    {
        public static readonly LinearActivator
            Current = new LinearActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return 1;
            }
        }
    }
}
