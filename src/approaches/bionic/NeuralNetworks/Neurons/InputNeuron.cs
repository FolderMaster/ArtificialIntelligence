namespace NeuralNetworks.Neurons
{
    public class InputNeuron : IInputNeuron
    {
        public double OutputValue { get; private set; }

        public event EventHandler<double> Activated;

        public double Activate(double value)
        {
            OutputValue = value;
            Activated?.Invoke(this, value);
            return value;
        }
    }
}
