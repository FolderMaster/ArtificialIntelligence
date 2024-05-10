using SemanticNetwork.Entities;
using SemanticNetwork.Relationships;

namespace SemanticNetwork
{
    public class SemanticSystem
    {
        private List<IEntity> _entities = new();

        private List<IRelationship> _relationships = new();

        public IEnumerable<IEntity> Entities => _entities;

        public IEnumerable<IRelationship> Relationships => _relationships;

        public SemanticSystem() { }

        public void AddEntity(IEntity entity)
        {

        }

        public void RemoveEntity(IEntity entity)
        {

        }

        public void AddRelationship(IRelationship relationship,
            IEnumerable<IEntity> entities1, IEnumerable<IEntity> entities2)
        {

        }

        public void RemoveRelationship(IRelationship relationship)
        {

        }
    }
}
