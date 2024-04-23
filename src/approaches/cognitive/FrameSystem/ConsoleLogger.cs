namespace FrameSystem
{
    public class ConsoleLogger : ILogger
    {
        public void Log(object? message = null) =>
            Console.WriteLine(message);
    }
}
