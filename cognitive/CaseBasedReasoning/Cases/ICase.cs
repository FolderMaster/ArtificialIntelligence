namespace CaseBasedReasoning.Cases
{
    public interface ICase
    {
        public object Task { get; }

        public object Solution { get; }
    }
}
