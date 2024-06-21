namespace NeuralNetworks.Layers.Activators
{
    public class PReLuActivator : IActivator
    {
        public const double a = 0.1;

        public static readonly PReLuActivator
            Current = new PReLuActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return double.Max(a * value, 0);
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value > 0 ? a : 0;
            }
        }
    }
}
