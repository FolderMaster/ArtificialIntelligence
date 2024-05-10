using SemanticNetwork.Relationships;

namespace SemanticNetwork.Entities
{
    public interface IEntity
    {
        public string Name { get; }

        public IList<IRelationship> Relationships { get; }
    }
}
