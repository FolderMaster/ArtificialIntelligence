namespace NeuralNetworks.Neurons.Activators
{
    public interface IActivator
    {
        public double Function(double value);

        public double Derivative(double value);
    }
}
