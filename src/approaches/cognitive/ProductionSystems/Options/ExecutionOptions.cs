namespace ProductionSystems.Options
{
    public class ExecutionOptions
    {
        public ExecutionMode ExecutionMode { get; set; }

        public OutputMode OutputMode { get; set; }

        public OutputDataMode OutputDataMode { get; set; }

        public IEnumerable<string>? Targets { get; set; }

        public ExecutionOptions() { }

        public ExecutionOptions(ExecutionMode executionMode,
            OutputMode outputMode, OutputDataMode outputDataMode,
            IEnumerable<string>? targets)
        {
            ExecutionMode = executionMode;
            OutputMode = outputMode;
            OutputDataMode = outputDataMode;
            Targets = targets;
        }
    }
}
