namespace NeuralNetworks.Layers
{
    public interface IInputNeuralLayer : INeuralLayer
    {
        public void Activate(IEnumerable<double> values);
    }
}
