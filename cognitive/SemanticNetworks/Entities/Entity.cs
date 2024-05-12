using SemanticNetworks.Relationships;

namespace SemanticNetworks.Entities
{
    public class Entity : IEntity
    {
        private readonly List<IRelationship> _relationships = new();

        public string? Name { get; private set; }

        public IList<IRelationship> Relationships => _relationships;

        public Entity(string? name = null) => Name = name;

        public override string ToString() => $"Entity {Name}";
    }
}
