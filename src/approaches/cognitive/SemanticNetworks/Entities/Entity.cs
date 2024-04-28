namespace SemanticNetwork.Entities
{
    public class Entity : IEntity
    {
        public string Name { get; private set; }

        public Entity(string name) => Name = name;
    }
}
