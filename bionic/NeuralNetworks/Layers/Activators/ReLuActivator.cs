namespace NeuralNetworks.Layers.Activators
{
    public class ReLuActivator : IActivator
    {
        public static readonly ReLuActivator
            Current = new ReLuActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return double.Max(value, 0);
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value > 0 ? 1 : 0;
            }
        }
    }
}
