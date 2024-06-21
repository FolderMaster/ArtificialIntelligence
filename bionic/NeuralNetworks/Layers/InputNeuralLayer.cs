namespace NeuralNetworks.Layers
{
    public class InputNeuralLayer : IInputNeuralLayer
    {
        public int NeuronsCount { get; private set; }

        public IEnumerable<double> Values { get; private set; }

        public event EventHandler<IEnumerable<double>>? Activated;

        public InputNeuralLayer(int neuronCount) =>
            NeuronsCount = neuronCount;

        public void Activate(IEnumerable<double> values)
        {
            Values = values;
            Activated?.Invoke(this, Values);
        }
    }
}
