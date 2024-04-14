namespace NeuralNetworks.Neurons.Activators
{
    public class TanhActivator : IActivator
    {
        public static readonly TanhActivator
            Current = new TanhActivator();

        public double Function(double value) =>
            Math.Tanh(value);

        public double Derivative(double value)
        {
            var a = Math.Tanh(value);
            return 1 - a * a;
        }
    }
}
