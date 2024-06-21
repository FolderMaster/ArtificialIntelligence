namespace NeuralNetworks.Layers.Activators
{
    public class LeakyReLuActivator : IActivator
    {
        public const double a = 0.1;

        public static readonly LeakyReLuActivator
            Current = new LeakyReLuActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value > 0 ? value : a * value;
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value > 0 ? 1 : a;
            }
        }
    }
}
