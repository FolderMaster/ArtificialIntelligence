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

        private double GenerateRandomValue(Random random) =>
            random.NextDouble() * random.Next(-1, 2);

        private void PreviousLayer_Activated(object? sender, IEnumerable<double> values)
        {
            for (var i = 0; i < NeuronsCount; ++i)
            {
                var weightedValues = new double[LinksCount];
                for (var j = 0; j < LinksCount; ++j)
                {
                    weightedValues[j] = _weights[i][j] * values.ElementAt(j);
                }
                _values[i] = weightedValues.Sum() + _biases[i];
            }
            Array.Copy(Activator.Function(_values).ToArray(), _values, NeuronsCount);
            Activated?.Invoke(this, _values);
        }
    }
}
