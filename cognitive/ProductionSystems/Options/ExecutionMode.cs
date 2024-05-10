namespace ProductionSystems.Options
{
    [Flags]
    public enum ExecutionMode
    {
        StandardSearch = 0,
        RuleWithLeastFactsSearch = 1 << 0,
        RemoveUselessRules = 1 << 1,
        TreeTraversal = 1 << 2
    }
}
