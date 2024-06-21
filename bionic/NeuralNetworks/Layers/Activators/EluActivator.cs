
namespace NeuralNetworks.Layers.Activators
{
    public class EluActivator : IActivator
    {
        public const double a = 1;

        public static readonly EluActivator
            Current = new EluActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value >= 0 ? value : a * (Math.Exp(value) - 1);
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return value >= 0 ? 1 : a * Math.Exp(value);
            }
        }
    }
}
