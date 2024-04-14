namespace NeuralNetworks.Neurons.Activators
{
    public class EluActivator : IActivator
    {
        public const double a = 1;

        public static readonly EluActivator
            Current = new EluActivator();

        public double Function(double value)
        {
            if(value >= 0)
            {
                return value;
            }
            return a * (Math.Exp(value) - 1);
        }

        public double Derivative(double value)
        {
            if (value >= 0)
            {
                return 1;
            }
            return a * Math.Exp(value);
        }
    }
}
