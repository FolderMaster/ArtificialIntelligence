using SemanticNetwork.Entities;

namespace SemanticNetwork.Relationships
{
    public interface IRelationship
    {
        public IList<IEntity> Entities { get; }
    }
}
