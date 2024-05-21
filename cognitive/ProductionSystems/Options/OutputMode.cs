namespace ProductionSystems.Options
{
    [Flags]
    public enum OutputMode
    {
        StandardOutput = 0,
        OnlySuccessfulSteps = 1 << 0,
        WithAdditionalOutput = 1 << 1
    }
}
