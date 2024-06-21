namespace NeuralNetworks.Layers.Activators
{
    public class SoftSignActivator : IActivator
    {
        public static readonly SoftSignActivator
            Current = new SoftSignActivator();

        public double Function(double value) =>
            value / (1 + Math.Abs(value));

        public double Derivative(double value)
        {
            var d = 1 + Math.Abs(value);
            return 1 / (d * d);
        }
    }
}
