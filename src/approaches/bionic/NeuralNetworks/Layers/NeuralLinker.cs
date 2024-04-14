using NeuralNetworks.Neurons;
using System.Collections;

namespace NeuralNetworks.Layers
{
    public class NeuralLinker
    {
        private readonly double[] _inputValues;

        private BitArray _neuronsFlags;

        public IEnumerable<double> InputValues => _inputValues;

        public INeuron[] InputNeurons { get; private set; }

        public IEnumerable<IOutputNeuron> OutputNeurons { get; private set; }

        public NeuralLinker(IEnumerable<INeuron> inputNeurons,
            IEnumerable<IOutputNeuron> outputNeurons)
        {
            InputNeurons = inputNeurons.ToArray();
            OutputNeurons = outputNeurons;
            _neuronsFlags = new BitArray(InputNeurons.Length, false);
            _inputValues = new double[InputNeurons.Count()];
            foreach (var neuron in InputNeurons)
            {
                neuron.Activated += Neuron_Activated;
            }
        }

        private void Neuron_Activated(object? sender, double e)
        {
            var neuron = sender as INeuron;
            var index = Array.IndexOf(InputNeurons, neuron);
            _inputValues[index] = e;
            _neuronsFlags[index] = true;
            if (_neuronsFlags.HasAllSet())
            {
                foreach (var outputNeuron in OutputNeurons)
                {
                    outputNeuron.Activate(_inputValues);
                }
                _neuronsFlags.SetAll(false);
            }
        }
    }
}
