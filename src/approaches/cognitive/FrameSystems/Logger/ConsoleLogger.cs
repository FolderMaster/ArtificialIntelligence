namespace FrameSystem.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(object? message = null) =>
            Console.WriteLine(message);

        public void Setup(IDictionary<string, object>? options = null)
        {
            if (options != null)
            {
                if (options.ContainsKey("Background"))
                {
                    Console.BackgroundColor = (ConsoleColor)options["Background"];
                }
                if (options.ContainsKey("Foreground"))
                {
                    Console.ForegroundColor = (ConsoleColor)options["Foreground"];
                }
            }
            else
            {
                Console.ResetColor();
            }
        }

        public void Log(object? message = null,
            IDictionary<string, object>? options = null)
        {
            Setup(options);
            Log(message);
            Setup();
        }
            
    }
}
