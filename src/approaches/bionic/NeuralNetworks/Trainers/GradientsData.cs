namespace NeuralNetworks.Trainers
{
    public class GradientsData
    {
        public double[] BiasesGradients { get; set; }

        public double[][] WeightsGradients { get; set; }

        /**public double[] BiasVelocities { get; set; }**/

        /**public double[][] WeightsVelocities { get; set; }**/

        public GradientsData(int inputCount, int outputCount)
        {
            BiasesGradients = new double[outputCount];
            /**BiasVelocities = new double[outputCount];**/
            WeightsGradients = new double[outputCount][];
            /**WeightsVelocities = new double[outputCount][];**/
            for (int i = 0; i < outputCount; ++i)
            {
                WeightsGradients[i] = new double[inputCount];
                /**WeightsVelocities[i] = new double[inputCount];**/
            }
        }
    }
}
