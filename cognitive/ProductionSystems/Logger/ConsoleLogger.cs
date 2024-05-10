namespace ProductionSystems.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(object? message = null) =>
            Console.WriteLine(message);

        public void Setup(IDictionary<string, object>? options = null)
        {
            if (options != null)
            {
                if (options.ContainsKey("Backgroud"))
                {
                    Console.BackgroundColor = (ConsoleColor)options["Backgroud"];
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
    }
}
