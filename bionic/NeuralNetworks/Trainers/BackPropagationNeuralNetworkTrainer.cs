using NeuralNetworks.Layers;
using NeuralNetworks.Network;
using NeuralNetworks.Trainers.Costs;

namespace NeuralNetworks.Trainers
{
    public class BackPropagationNeuralNetworkTrainer : INeuralNetworkTrainer
    {
        public ICost Cost { get; set; } = MSECost.Current;

        public NeuralNetwork? NeuralNetwork { get; set; }

        public IEnumerable<IEnumerable<double>>? Input { get; set; }

        public IEnumerable<IEnumerable<double>>? Target { get; set; }

        public double LearningRate { get; set; } = 0.05;

        public double GradientClip { get; set; } = 5;

        /** public double Momentum { get; set; } = 0.9; **/

        /** public double Regularization { get; set; } = 0.15; **/

        public double? EpochsCount { get; set; } = 100;

        public double? MaximumError { get; set; } = 0.01;

        public void Train()
        {
            ArgumentNullException.ThrowIfNull(Input);
            ArgumentNullException.ThrowIfNull(Target);
            ArgumentNullException.ThrowIfNull(NeuralNetwork);

            var layersGradientsData = GenerateLayersGradientsData();
            var data = Input.Zip(Target).ToArray();
            var dataCount = data.Length;
            var totalError = 1d;
            for(var e = 0; CheckEpochNecessity(totalError, e); ++e)
            {
                totalError = 0d;
                Parallel.ForEach(data, (d) =>
                {
                    IEnumerable<double> prediction;
                    IEnumerable<IEnumerable<double>> outputsData;
                    lock (NeuralNetwork)
                    {
                        prediction = NeuralNetwork.Predict(d.First);
                        outputsData = ShotValues();
                    }
                    totalError += Cost.Function(prediction, d.Second);
                    BackPropagate(d.Second, layersGradientsData, outputsData);
                });
                totalError /= dataCount;
                Console.WriteLine($"Epoch {e + 1}, Total Error: {totalError}");
                FixLayersGradientsData(layersGradientsData, dataCount);
                UpdateWeightsAndBias(layersGradientsData);
                ClearLayersGradientsData(layersGradientsData);
            }
        }

        private IEnumerable<GradientsData> GenerateLayersGradientsData()
        {
            var layers = NeuralNetwork.OutputLayers;
            var layersCount = layers.Count();
            var result = new GradientsData[layersCount];
            for(int i = 0; i < layersCount; ++i)
            {
                var layer = layers.ElementAt(i);
                var outputNeuronsCount = layer.NeuronsCount;
                var inputNeuronsCount = layer.LinksCount;
                result[i] = new GradientsData(inputNeuronsCount,
                    outputNeuronsCount);
            }
            return result;
        }

        private IEnumerable<IEnumerable<double>> ShotValues()
        {
            var layers = NeuralNetwork.OutputLayers;
            var layersCount = layers.Count();
            var result = new double[layersCount + 1][];
            result[0] = NeuralNetwork.InputLayer.Values.ToArray();
            for (int i = 0; i < layersCount; ++i)
            {
                result[i + 1] = layers.ElementAt(i).Values.ToArray();
            }
            return result;
        }

        private void BackPropagate(IEnumerable<double> target,
            IEnumerable<GradientsData> layersGradientsData,
            IEnumerable<IEnumerable<double>> outputsData)
        {
            var layers = NeuralNetwork.OutputLayers;
            var layersCount = layers.Count();
            var gradients = CalculateOutputGradients(layers.Last(), target,
                layersGradientsData.ElementAt(layersCount - 1),
                outputsData.ElementAt(layersCount - 1),
                outputsData.ElementAt(layersCount));
            for(int i = layersCount - 1; i >= 1; --i)
            {
                var nextLayer = layers.ElementAt(i);
                var hiddenLayer = layers.ElementAt(i - 1);
                gradients = CalculateHiddenGradients(hiddenLayer, nextLayer,
                    gradients, layersGradientsData.ElementAt(i - 1),
                    outputsData.ElementAt(i - 1), outputsData.ElementAt(i));
            }
        }

        private double[] CalculateOutputGradients(OutputNeuralLayer layer,
            IEnumerable<double> target, GradientsData gradientsData,
            IEnumerable<double> inputsData, IEnumerable<double> outputsData)
        {
            var count = layer.NeuronsCount;
            var result = new double[count];
            var activation = layer.Activator.Derivative(outputsData);
            for (int i = 0; i < count; i++)
            {
                var cost = Cost.Derivative(outputsData.ElementAt(i),
                    target.ElementAt(i));
                result[i] = cost * activation.ElementAt(i);
            }
            lock (gradientsData)
            {
                for (int i = 0; i < count; ++i)
                {
                    gradientsData.BiasesGradients[i] += result[i];
                    for (int j = 0; j < inputsData.Count(); ++j)
                    {
                        gradientsData.WeightsGradients[i][j] += result[i] *
                            inputsData.ElementAt(j);
                    }
                }
            }
            return result;
        }

        public double GetCostSecondDerivative
            (IEnumerable<double> weights,
            IEnumerable<double> gradients)
        {
            var cost = 0d;
            for (var i = 0; i < weights.Count(); ++i)
            {
                cost += weights.ElementAt(i) *
                    gradients.ElementAt(i);
            }
            return cost;
        }

        private double[] CalculateHiddenGradients(OutputNeuralLayer hiddenLayer,
            OutputNeuralLayer nextLayer, IEnumerable<double> gradients,
            GradientsData gradientsData, IEnumerable<double> inputsData,
            IEnumerable<double> outputsData)
        {
            var result = new double[hiddenLayer.NeuronsCount];
            var activation = hiddenLayer.Activator.Derivative(outputsData);
            for (int i = 0; i < hiddenLayer.NeuronsCount; i++)
            {
                var weights = new double[nextLayer.NeuronsCount];
                for (int j = 0; j < nextLayer.NeuronsCount; j++)
                {
                    weights[j] = nextLayer.Weights.ElementAt(j).ElementAt(i);
                }
                var cost = GetCostSecondDerivative(weights, gradients);
                result[i] = cost * activation.ElementAt(i);
            }
            for (int i = 0; i < hiddenLayer.NeuronsCount; ++i)
            {
                gradientsData.BiasesGradients[i] += result[i];
                for (int j = 0; j < inputsData.Count(); ++j)
                {
                    gradientsData.WeightsGradients[i][j] += result[i] *
                        inputsData.ElementAt(j);
                }
            }
            return result;
        }

        private void FixLayersGradientsData
            (IEnumerable<GradientsData> layersGradientsData, int dataCount)
        {
            foreach (var gradientsData in layersGradientsData)
            {
                for (var i = 0; i < gradientsData.BiasesGradients.Length; ++i)
                {
                    gradientsData.BiasesGradients[i] =
                        FixGradientValue(gradientsData.BiasesGradients[i], dataCount);
                }

                for (var i = 0; i < gradientsData.WeightsGradients.Length; ++i)
                {
                    for (var j = 0; j < gradientsData.WeightsGradients[i].Length; ++j)
                    {
                        gradientsData.WeightsGradients[i][j] =
                            FixGradientValue(gradientsData.WeightsGradients[i][j], dataCount);
                    }
                }
            }
        }

        private double FixGradientValue(double value, int dataCount)
        {
            var result = value / dataCount;
            if(Math.Abs(result) > GradientClip)
            {
                if(result > 0)
                {
                    return GradientClip;
                }
                return -GradientClip;
            }
            return result;
        }

        private void UpdateWeightsAndBias
            (IEnumerable<GradientsData> layersGradientsData)
        {
            /**var weightDecay = (1 - Regularization * LearningRate);**/
            
            var data = NeuralNetwork.OutputLayers.Zip(layersGradientsData);
            foreach(var value in data)
            {
                UpdateLayerWeightsAndBias(value.First,
                    value.Second/**, weightDecay**/);
            }
        }

        private void UpdateLayerWeightsAndBias(OutputNeuralLayer layer,
            GradientsData gradientsData/**, double weightDecay**/)
        {
            var outputNeuronsCount = layer.NeuronsCount;
            var inputNeuronsCount = layer.LinksCount;
            for (var i = 0; i < outputNeuronsCount; ++i)
            {
                /**var biasVelocity = gradientsData.BiasVelocities[i] *
                        Momentum - gradientsData.BiasGradients[i] *
                        LearningRate;
                gradientsData.BiasVelocities[i] = biasVelocity;
                neuron.Bias += biasVelocity;**/
                
                layer.Biases[i] -= LearningRate * gradientsData.BiasesGradients[i];
                for (var j = 0; j < inputNeuronsCount; ++j)
                {
                    /**var weightVelocity = gradientsData.WeightsVelocities[i][j] *
                        Momentum - gradientsData.WeightsGradients[i][j] *
                        LearningRate;
                    gradientsData.WeightsVelocities[i][j] = weightVelocity;
                    neuron.Weights[j] = neuron.Weights[j] *
                        weightDecay + weightVelocity;**/
                    layer.Weights[i][j] -= LearningRate *
                        gradientsData.WeightsGradients[i][j];
                }
            }
        }

        private void ClearLayersGradientsData
            (IEnumerable<GradientsData> layersGradientsData)
        {
            foreach (var gradientsData in layersGradientsData)
            {
                for (var i = 0; i < gradientsData.BiasesGradients.Length; ++i)
                {
                    gradientsData.BiasesGradients[i] = 0;
                }

                for (var i = 0; i < gradientsData.WeightsGradients.Length; ++i)
                {
                    for (var j = 0; j < gradientsData.WeightsGradients[i].Length; ++j)
                    {
                        gradientsData.WeightsGradients[i][j] = 0;
                    }
                }
            }
        }

        private bool CheckEpochNecessity(double totalError, int epochesCount) =>
            (MaximumError == null || totalError > MaximumError) &&
            (EpochsCount == null || epochesCount < EpochsCount);
    }
}
