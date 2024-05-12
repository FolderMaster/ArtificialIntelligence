using CaseBasedReasoning.Cases;

namespace CaseBasedReasoning.Systems
{
    public abstract class BaseCaseBasedSystem : ICaseBasedSystem
    {
        protected readonly HashSet<ICase> _cases = new();

        public IEnumerable<ICase> Cases => _cases;

        protected abstract IEnumerable<ICase> Retrieve(object task);

        protected abstract object? Reuse(IEnumerable<ICase> cases);

        protected abstract bool CheckCase(ICase @case);

        public object? GetSolutionForTask(object task)
            => Reuse(Retrieve(task));

        public void Retain(ICase @case)
        {
            if (CheckCase(@case))
            {
                _cases.Add(@case);
            }
        }
    }
}
