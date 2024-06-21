namespace NeuralNetworks.Layers.Activators
{
    internal class LinearActivator : IActivator
    {
        public static readonly LinearActivator
            Current = new LinearActivator();

        public double Function(double value) => value;

        public double Derivative(double value) => 1;
    }
}
