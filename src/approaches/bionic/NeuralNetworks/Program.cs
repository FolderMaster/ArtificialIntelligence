using NeuralNetworks.Network;
using NeuralNetworks.Neurons.Activators;
using NeuralNetworks.Trainers;
using NeuralNetworks.Trainers.Costs;

var neuronNetwork = new NeuralNetwork(2);
neuronNetwork.AddLayer(2, TanhActivator.Current);
neuronNetwork.AddLayer(4, SoftPlusActivator.Current);
Print(neuronNetwork);

var data = GetData(10000);
var input = data.Select((d) => new double[]
{ d.Item1.X, d.Item1.Y }).ToArray();
var target = data.Select((d) => new double[]
{ d.Item2.Quarter1, d.Item2.Quarter2,
        d.Item2.Quarter3, d.Item2.Quarter4 }).ToArray();
var trainer = new BackPropagationNeuralNetworkTrainer()
{
    Target = target,
    Input = input,
    NeuralNetwork = neuronNetwork,
    LearningRate = 1,
    EpochsCount = 100,
    MaximumError = 0.01,
    Cost = MSECost.Current
};
trainer.Train();
Print(neuronNetwork);

Console.WriteLine();
foreach (var item in GetData(10))
{
    var point = item.Item1;
    Console.WriteLine("Point:");
    Console.WriteLine(point);
    var expected = item.Item2;
    Console.WriteLine("Expected:");
    Console.WriteLine(expected);
    var predicted = neuronNetwork.Predict([point.X, point.Y]);
    Console.WriteLine("Predicted:");
    Console.WriteLine(new PointCharacteristic(predicted.ElementAt(0),
        predicted.ElementAt(1), predicted.ElementAt(2),
        predicted.ElementAt(3)));
    Console.WriteLine();
}

void Print(NeuralNetwork neuronNetwork)
{
    var inputLayers = neuronNetwork.InputLayer;
    Console.WriteLine("Intput layer:");
    Console.WriteLine($"Neurons: {neuronNetwork.
        InputLayer.Neurons.Count()}");
    Console.WriteLine();
    Console.WriteLine();

    var outputLayers = neuronNetwork.OutputLayers;
    for (var i = 0; i < outputLayers.Count(); ++i)
    {
        Console.WriteLine($"Output layer {i + 1}:");
        var layer = outputLayers.ElementAt(i);
        for (var j = 0; j < layer.Neurons.Count(); ++j)
        {
            var neuron = layer.OutputNeurons.ElementAt(j);
            Console.WriteLine($"Neuron {j + 1}:");
            Console.WriteLine($"Bias: {neuron.Bias}");
            Console.WriteLine($"Weights: {string.Join(", ",
                neuron.Weights)}");
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

IEnumerable<(Point, PointCharacteristic)> GetData(int count)
{
    var rand = new Random();
    var result = new (Point, PointCharacteristic)[count];
    for (int i = 0; i < count; ++i)
    {
        var x = 10 * rand.NextDouble() * GetSign(rand);
        var y = 10 * rand.NextDouble() * GetSign(rand);
        var q1 = x > 0 && y > 0 ? 1 : 0;
        var q2 = x < 0 && y > 0 ? 1 : 0;
        var q3 = x < 0 && y < 0 ? 1 : 0;
        var q4 = x > 0 && y < 0 ? 1 : 0;
        result[i] = (new Point(x, y),
            new PointCharacteristic(q1, q2, q3, q4));
    }
    return result;
}

double GetSign(Random rand)
{
    var value = rand.Next(-1, 2);
    return (value < 0) ? -1 : 1;
}

public class Point
{
    public double X { get; set; }

    public double Y { get; set; }

    public Point() { }

    public Point(double x, double y)
    { 
        X = x;
        Y = y;
    }

    public override string ToString() => $"X: {X}\nY: {Y}";
}

public class PointCharacteristic
{
    public double Quarter1 { get; set; }

    public double Quarter2 { get; set; }

    public double Quarter3 { get; set; }

    public double Quarter4 { get; set; }

    public PointCharacteristic() { }

    public PointCharacteristic(double quarter1, double quarter2,
        double quarter3, double quarter4)
    {
        Quarter1 = quarter1;
        Quarter2 = quarter2;
        Quarter3 = quarter3;
        Quarter4 = quarter4;
    }

    public override string ToString() => $"1: {Quarter1}\n2: " +
        $"{Quarter2}\n3: {Quarter3}\n4: {Quarter4}";
}