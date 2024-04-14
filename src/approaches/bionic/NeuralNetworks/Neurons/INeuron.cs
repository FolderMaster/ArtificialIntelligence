namespace NeuralNetworks.Neurons
{
    public interface INeuron
    {
        public event EventHandler<double> Activated;

        public double OutputValue { get; }
    }
}
