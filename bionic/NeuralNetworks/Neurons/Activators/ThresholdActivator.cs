namespace NeuralNetworks.Neurons.Activators
{
    public class ThresholdActivator : IActivator
    {
        public static readonly ThresholdActivator
            Current = new ThresholdActivator();

        public double Function(double value) =>
            value >= 0 ? 1 : 0;

        public double Derivative(double value) => 0;
    }
}
