namespace NeuralNetworks.Neurons.Activators
{
    public class SigmoidActivator : IActivator
    {
        public static readonly SigmoidActivator
            Current = new SigmoidActivator();

        public double Function(double value) =>
            1.0 / (1 + Math.Exp(-value));

        public double Derivative(double value)
        {
            double a = Function(value);
            return a * (1 - a);
        }
    }
}
