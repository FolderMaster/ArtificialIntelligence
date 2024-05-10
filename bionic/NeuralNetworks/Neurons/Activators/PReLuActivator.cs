namespace NeuralNetworks.Neurons.Activators
{
    public class PReLuActivator : IActivator
    {
        public const double a = 0.1;

        public static readonly PReLuActivator
            Current = new PReLuActivator();

        public double Function(double value) =>
            double.Max(a * value, 0);

        public double Derivative(double value) =>
            value > 0 ? a : 0;
    }
}
