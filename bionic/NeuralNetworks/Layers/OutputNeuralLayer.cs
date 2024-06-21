using NeuralNetworks.Layers.Activators;

namespace NeuralNetworks.Layers
{
    public class OutputNeuralLayer : IOutputNeuralLayer
    {
        private readonly double[][] _weights;

        private readonly double[] _biases;

        private readonly double[] _values;

        public int NeuronsCount { get; private set; }

        public int LinksCount { get; private set; }

        public IEnumerable<double> Values => _values;

        public IList<double> Biases => _biases;

        public IList<IList<double>> Weights => _weights;

        public IActivator Activator { get; private set; }

        public event EventHandler<IEnumerable<double>> Activated;

        public OutputNeuralLayer(int neuronCount, IActivator activator,
            INeuralLayer previousLayer)
        {
            if(neuronCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(neuronCount));
            }
            ArgumentNullException.ThrowIfNull(activator);
            ArgumentNullException.ThrowIfNull(previousLayer);

            NeuronsCount = neuronCount;
            Activator = activator;

            var random = new Random();
            LinksCount = previousLayer.NeuronsCount;

            _biases = new double[neuronCount];
            _weights = new double[neuronCount][];
            _values = new double[neuronCount];
            for (int i = 0; i < neuronCount; ++i)
            {
                _biases[i] = GenerateRandomValue(random);
                _weights[i] = new double[LinksCount];
                for (int j = 0; j < LinksCount; ++j)
                {
                    _weights[i][j] = GenerateRandomValue(random);
                }
            }
            previousLayer.Activated += PreviousLayer_Activated;
        }

        private void CalculateOutputValue(IEnumerable<double> values, int index)
        {
            var weightedValues = new double[LinksCount];
            for (var i = 0; i < LinksCount; ++i)
            {
                weightedValues[i] = _weights[index][i] * values.ElementAt(i);
            }
            _values[index] = Activator.Function(weightedValues.Sum() + _biases[index]);
        }

        private double GenerateRandomValue(Random random) =>
            random.NextDouble() * random.Next(-1, 2);

        private void PreviousLayer_Activated(object? sender, IEnumerable<double> values)
        {
            for (var i = 0; i < NeuronsCount; ++i)
            {
                CalculateOutputValue(values, i);
            }
            Activated?.Invoke(this, _values);
        }
    }
}
