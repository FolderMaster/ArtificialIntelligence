using SemanticNetworks.Entities;

namespace SemanticNetworks.Relationships
{
    public class BaseRelationship : IRelationship
    {
        public virtual string? Name { get; }

        public IEntity? Entity1 { get; set; }

        public IEntity? Entity2 { get; set; }

        public override string ToString() =>
            $"Relationship {Name} ({Entity1}, {Entity2})";
    }
}
