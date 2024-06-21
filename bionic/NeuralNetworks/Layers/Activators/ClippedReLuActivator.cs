namespace NeuralNetworks.Layers.Activators
{
    class ClippedReLuActivator : IActivator
    {
        public static readonly ClippedReLuActivator
            Current = new ClippedReLuActivator();

        public double Function(double value) =>
            double.Min(double.Max(value, 0), 1);

        public double Derivative(double value) =>
            value > 0 && value < 1 ? 1 : 0;
    }
}
