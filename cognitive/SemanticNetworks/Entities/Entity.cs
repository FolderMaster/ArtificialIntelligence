using SemanticNetwork.Relationships;

namespace SemanticNetwork.Entities
{
    public class Entity : IEntity
    {
        public string Name { get; private set; }

        public IList<IRelationship> Relationships => throw new NotImplementedException();

        public Entity(string name) => Name = name;
    }
}
