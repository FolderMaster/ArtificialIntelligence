namespace NeuralNetworks.Layers.Activators
{
    public interface IActivator
    {
        public IEnumerable<double> Function(IEnumerable<double> values);

        public IEnumerable<double> Derivative(IEnumerable<double> values);
    }
}
