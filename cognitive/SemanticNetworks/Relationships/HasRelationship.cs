using SemanticNetwork.Entities;

namespace SemanticNetwork.Relationships
{
    public class HasRelationship : IRelationship
    {
        public IList<IEntity> Entities => throw new NotImplementedException();
    }
}
