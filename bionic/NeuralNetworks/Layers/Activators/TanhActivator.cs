namespace NeuralNetworks.Layers.Activators
{
    public class TanhActivator : IActivator
    {
        public static readonly TanhActivator
            Current = new TanhActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return Math.Tanh(value);
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                var a = Math.Tanh(value);
                yield return 1 - a * a;
            }
        }
    }
}
