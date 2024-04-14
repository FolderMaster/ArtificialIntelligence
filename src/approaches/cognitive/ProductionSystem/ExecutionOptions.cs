namespace ProductionSystem
{
    public class ExecutionOptions
    {
        public ExecutionMode ExecutionMode { get; set; }

        public OutputMode OutputMode { get; set; }

        public OutputDataMode OutputDataMode { get; set; }

        public ExecutionOptions() { }

        public ExecutionOptions(ExecutionMode executionMode,
            OutputMode outputMode, OutputDataMode outputDataMode)
        {
            ExecutionMode = executionMode;
            OutputMode = outputMode;
            OutputDataMode = outputDataMode;
        }
    }
}
