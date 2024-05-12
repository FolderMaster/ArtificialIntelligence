using SemanticNetworks.Entities;
using SemanticNetworks.Relationships;

namespace SemanticNetworks.Groups
{
    public class SemanticSystem : IGroup
    {
        private List<IEntity> _entities = new();

        private List<IRelationship> _relationships = new();

        public IEnumerable<IEntity> Entities => _entities;

        public IEnumerable<IRelationship> Relationships => _relationships;

        public SemanticSystem() { }

        public void AddEntity(IEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _entities.Add(entity);
        }

        public void RemoveEntity(IEntity? entity) =>
            _entities.Remove(entity);

        public void AddRelationship(IRelationship relationship,
            IEntity entity1, IEntity entity2,
            bool isEntity1Connected = true, bool isEntity2Connected = true)
        {
            ArgumentNullException.ThrowIfNull(relationship,
                nameof(relationship));
            ArgumentNullException.ThrowIfNull(entity1,
                nameof(entity1));
            ArgumentNullException.ThrowIfNull(entity2,
                nameof(entity2));
            relationship.Entity1 = entity1;
            relationship.Entity2 = entity2;
            if (isEntity1Connected)
            {
                entity1.Relationships.Add(relationship);
            }
            if (isEntity2Connected)
            {
                entity2.Relationships.Add(relationship);
            }
            _relationships.Add(relationship);
        }

        public void RemoveRelationship(IRelationship? relationship)
        {
            _relationships.Remove(relationship);
            if (relationship.Entity1 != null)
            {
                relationship.Entity1.Relationships.
                    Remove(relationship);
            }
            if (relationship.Entity2 != null)
            {
                relationship.Entity2.Relationships.
                    Remove(relationship);
            }
        }

        public IEnumerable<IGroup> FindGroup(IGroup group)
        {
            var result = new List<IGroup>();

            return result;
        }

        public IEnumerable<IEntity> FindEntities(IEnumerable<IEntity> entities)
        {
            var result = new List<IEntity>();
            foreach (var currentEntity in Entities)
            {
                var isTrue = false;
                foreach (var entity in entities)
                {
                    if(entity == currentEntity)
                    {

                    }
                }
            }
            return result;
        }
    }
}
