using SemanticNetworks.Relationships;

namespace SemanticNetworks.Entities
{
    public interface IEntity
    {
        public string? Name { get; }

        public IList<IRelationship> Relationships { get; }
    }
}
