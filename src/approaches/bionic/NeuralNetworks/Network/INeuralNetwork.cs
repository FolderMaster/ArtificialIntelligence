namespace NeuralNetworks.Network
{
    public interface INeuralNetwork
    {
        public IEnumerable<double> Predict(IEnumerable<double> values);
    }
}
