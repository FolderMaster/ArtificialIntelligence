using SemanticNetworks.Entities;
using SemanticNetworks.Relationships;

namespace SemanticNetworks.Groups
{
    public interface IGroup
    {
        public IEnumerable<IEntity> Entities { get; }

        public IEnumerable<IRelationship> Relationships { get; }
    }
}
