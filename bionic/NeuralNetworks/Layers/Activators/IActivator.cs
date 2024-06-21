namespace NeuralNetworks.Layers.Activators
{
    public interface IActivator
    {
        public double Function(double value);

        public double Derivative(double value);
    }
}
