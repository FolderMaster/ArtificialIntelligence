namespace NeuralNetworks.Neurons
{
    public interface IInputNeuron : INeuron
    {
        public double Activate(double value);
    }
}
