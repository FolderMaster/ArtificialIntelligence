namespace NeuralNetworks.Layers.Activators
{
    public class SoftPlusActivator : IActivator
    {
        public static readonly SoftPlusActivator
            Current = new SoftPlusActivator();

        public IEnumerable<double> Function(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                yield return Math.Log(1 + Math.Exp(value));
            }
        }

        public IEnumerable<double> Derivative(IEnumerable<double> values)
        {
            foreach (var value in values)
            {
                var a = Math.Exp(value);
                yield return a / (1 + a);
            }
        }
    }
}
