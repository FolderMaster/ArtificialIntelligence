using CaseBasedReasoning.Cases;

namespace CaseBasedReasoning.Systems
{
    public interface ICaseBasedSystem
    {
        public object? GetSolutionForTask(object task);

        public void Retain(ICase @case);
    }
}
