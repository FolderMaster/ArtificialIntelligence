namespace NeuralNetworks.Layers
{
    public interface INeuralLayer
    {
        public int NeuronsCount { get; }

        public IEnumerable<double> Values { get; }

        public event EventHandler<IEnumerable<double>> Activated;
    }
}
