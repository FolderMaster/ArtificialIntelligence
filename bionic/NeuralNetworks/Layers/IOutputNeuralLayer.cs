using NeuralNetworks.Layers.Activators;

namespace NeuralNetworks.Layers
{
    public interface IOutputNeuralLayer : INeuralLayer
    {
        public int LinksCount { get; }

        public IList<double> Biases { get; }

        public IList<IList<double>> Weights { get; }

        public IActivator Activator { get; }
    }
}
