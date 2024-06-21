using NeuralNetworks.Layers;
using NeuralNetworks.Layers.Activators;

namespace NeuralNetworks.Network
{
    public class NeuralNetwork
    {
        private readonly List<OutputNeuralLayer> _outputLayers = new();

        public InputNeuralLayer InputLayer { get; private set; }

        public IEnumerable<OutputNeuralLayer> OutputLayers => _outputLayers;

        public NeuralNetwork(int neuronCount)
        {
            InputLayer = new InputNeuralLayer(neuronCount);
        }

        public void AddLayer(int neuronCount, IActivator? activator = null)
        {
            if(activator == null)
            {
                activator = SigmoidActivator.Current;
            }
            if (!OutputLayers.Any())
            {
                _outputLayers.Add(new OutputNeuralLayer(neuronCount, activator, InputLayer));
            }
            else
            {
                var layer = OutputLayers.Last();
                _outputLayers.Add(new OutputNeuralLayer(neuronCount, activator, layer));
            }
        }

        public IEnumerable<double> Predict(IEnumerable<double> values)
        {
            ArgumentNullException.ThrowIfNull(values);
            if(!_outputLayers.Any())
            {
                throw new InvalidOperationException();
            }
            InputLayer.Activate(values);
            return OutputLayers.Last().Values;
        }
    }
}
