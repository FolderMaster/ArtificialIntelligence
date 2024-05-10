using NeuralNetworks.Neurons.Activators;

namespace NeuralNetworks.Neurons
{
    public interface IOutputNeuron : INeuron
    {
        public IActivator Activator { get; }

        public double Bias { get; set; }

        public IList<double> Weights { get; set; }

        public double Activate(IEnumerable<double> values);
    }
}
