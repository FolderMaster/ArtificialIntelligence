namespace NeuralNetworks.Trainers
{
    public interface INeuralNetworkTrainer
    {
        public IEnumerable<IEnumerable<double>> Input { get; set; }

        public IEnumerable<IEnumerable<double>> Target { get; set; }

        public void Train();
    }
}
