namespace ProductionSystems.Logger
{
    public interface ILogger
    {
        public void Log(object? message = null);

        public void Setup(IDictionary<string, object>? options = null);
    }
}
