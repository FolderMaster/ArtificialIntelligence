using NeuralNetworks.Neurons.Activators;

namespace NeuralNetworks.Neurons
{
    public class OutputNeuron : IOutputNeuron
    {
        public IActivator Activator { get; private set; }

        public double Bias { get; set; }

        public double[] Weights { get; set; }

        public double OutputValue { get; private set; }

        public event EventHandler<double> Activated;

        public OutputNeuron(IActivator activator) => Activator = activator;

        public OutputNeuron(IActivator activator, IEnumerable<double> weights) :
            this(activator) => Weights = weights.ToArray();

        public OutputNeuron(IActivator activator,
            IEnumerable<double> weights, double bias) : this(activator, weights)
            => Bias = bias;

        public double Activate(IEnumerable<double> values)
        {
            ArgumentNullException.ThrowIfNull(Activator);
            ArgumentNullException.ThrowIfNull(Weights);
            ArgumentNullException.ThrowIfNull(values);

            var count = Weights.Count();
            var weightedValues = new double[count];
            for (var i = 0; i < count; ++i)
            {
                weightedValues[i] = Weights.ElementAt(i) * values.ElementAt(i);
            }
            OutputValue = Activator.Function(weightedValues.Sum() + Bias);
            Activated?.Invoke(this, OutputValue);
            return OutputValue;
        }
    }
}
