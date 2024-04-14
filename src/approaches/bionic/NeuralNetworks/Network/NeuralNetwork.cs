using NeuralNetworks.Layers;
using NeuralNetworks.Neurons.Activators;

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
            if (OutputLayers.Count() == 0)
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

            foreach (var item in InputLayer.InputNeurons.Zip(values))
            {
                item.First.Activate(item.Second);
            }
            var lastNeurons = OutputLayers.Last().Neurons;
            var lastNeuronsCount = lastNeurons.Count();
            var result = new double[lastNeuronsCount];
            for (var i = 0; i < lastNeuronsCount; ++i)
            {
                result[i] = lastNeurons.ElementAt(i).OutputValue;
            }
            return result;
        }
    }
}
