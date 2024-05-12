using SemanticNetworks.Entities;

namespace SemanticNetworks.Relationships
{
    public interface IRelationship
    {
        public string? Name { get; }

        public IEntity? Entity1 { get; set; }

        public IEntity? Entity2 { get; set; }
    }
}
