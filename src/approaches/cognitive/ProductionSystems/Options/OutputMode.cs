namespace ProductionSystems.Options
{
    [Flags]
    public enum OutputMode
    {
        StandardOutput = 0,
        OutgoingOutput = 1 << 0,
        OutboundOutput = 1 << 1,
        OnlySuccessfulSteps = 1 << 2
    }
}
