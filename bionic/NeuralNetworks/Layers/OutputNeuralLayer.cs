using NeuralNetworks.Neurons;
using NeuralNetworks.Neurons.Activators;

namespace NeuralNetworks.Layers
{
    public class OutputNeuralLayer : INeuralLayer
    {
        private readonly Random _random = new();

        public NeuralLinker Linker { get; private set; }

        public IEnumerable<IOutputNeuron> OutputNeurons { get; private set; }

        public IEnumerable<INeuron> Neurons => OutputNeurons;

        public OutputNeuralLayer(int neuronCount, IActivator activator,
            INeuralLayer previousLayer)
        {
            ArgumentNullException.ThrowIfNull(activator);
            ArgumentNullException.ThrowIfNull(previousLayer);

            var linksCount = previousLayer.Neurons.Count();
            var neurons = new IOutputNeuron[neuronCount];
            var bias = _random.NextDouble() * _random.Next(-1, 2);
            for (int i = 0; i < neuronCount; ++i)
            {
                
                var weights = new List<double>();
                for (var j = 0; j < linksCount; ++j)
                {
                    weights.Add(_random.NextDouble() * _random.Next(-1, 2));
                }
                neurons[i] = new OutputNeuron(activator, weights, bias);
            }
            OutputNeurons = neurons;
            Linker = new NeuralLinker(previousLayer.Neurons, OutputNeurons);
        }
    }
}
