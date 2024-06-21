
namespace NeuralNetworks.Layers.Activators
{
    class ClippedReLuActivator : IActivator
    {
        public static readonly ClippedReLuActivator
            Current = new ClippedReLuActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return double.Min(double.Max(value, 0), 1);
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value > 0 && value < 1 ? 1 : 0;
            }
        }

        private ClippedReLuActivator() { }
    }
}
