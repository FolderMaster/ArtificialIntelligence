namespace NeuralNetworks.Layers.Activators
{
    public class SoftPlusActivator : IActivator
    {
        public static readonly SoftPlusActivator
            Current = new SoftPlusActivator();

        public double Function(double value) =>
            Math.Log(1 + Math.Exp(value));

        public double Derivative(double value)
        {
            double a = Math.Exp(value);
            return a / (1 + a);
        }
    }
}
