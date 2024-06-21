namespace NeuralNetworks.Layers.Activators
{
    public class EluActivator : IActivator
    {
        public const double a = 1;

        public static readonly EluActivator
            Current = new EluActivator();

        public double Function(double value) =>
            value >= 0 ? value : a * (Math.Exp(value) - 1);

        public double Derivative(double value) =>
            value >= 0 ? 1 : a * Math.Exp(value);
    }
}
