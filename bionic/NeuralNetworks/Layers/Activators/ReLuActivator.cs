namespace NeuralNetworks.Layers.Activators
{
    public class ReLuActivator : IActivator
    {
        public static readonly ReLuActivator
            Current = new ReLuActivator();

        public double Function(double value) =>
            double.Max(value, 0);

        public double Derivative(double value) =>
            value > 0 ? 1 : 0;
    }
}
