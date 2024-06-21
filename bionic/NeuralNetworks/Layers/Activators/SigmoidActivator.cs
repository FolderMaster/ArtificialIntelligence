namespace NeuralNetworks.Layers.Activators
{
    public class SigmoidActivator : IActivator
    {
        public static readonly SigmoidActivator
            Current = new SigmoidActivator();

        private double Function(double value) =>
            1.0 / (1 + Math.Exp(-value));

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return Function(value);
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                var a = Function(value);
                yield return a * (1 - a);
            }
        }
    }
}
