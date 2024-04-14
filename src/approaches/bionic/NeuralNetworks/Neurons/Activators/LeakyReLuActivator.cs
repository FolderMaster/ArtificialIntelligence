namespace NeuralNetworks.Neurons.Activators
{
    public class LeakyReLuActivator : IActivator
    {
        public const double a = 0.1;

        public static readonly LeakyReLuActivator
            Current = new LeakyReLuActivator();

        public double Function(double value) =>
            value > 0 ? value : a * value;

        public double Derivative(double value) =>
            value > 0 ? 1 : a;
    }
}
