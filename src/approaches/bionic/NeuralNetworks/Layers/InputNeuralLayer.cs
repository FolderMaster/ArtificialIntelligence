using NeuralNetworks.Neurons;

namespace NeuralNetworks.Layers
{
    public class InputNeuralLayer : INeuralLayer
    {
        public IEnumerable<IInputNeuron> InputNeurons { get; private set; }

        public IEnumerable<INeuron> Neurons => InputNeurons;

        public InputNeuralLayer(int neuronCount)
        {
            var neurons = new IInputNeuron[neuronCount];
            for (int i = 0; i < neuronCount; i++)
            {
                neurons[i] = new InputNeuron();
            }
            InputNeurons = neurons;
        }
    }
}
