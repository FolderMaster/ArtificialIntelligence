using NeuralNetworks.Neurons;

namespace NeuralNetworks.Layers
{
    public interface INeuralLayer
    {
        public IEnumerable<INeuron> Neurons { get; }
    }
}
