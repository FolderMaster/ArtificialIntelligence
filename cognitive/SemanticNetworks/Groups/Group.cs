using SemanticNetworks.Entities;
using SemanticNetworks.Relationships;

namespace SemanticNetworks.Groups
{
    public class Group : IGroup
    {
        public IEnumerable<IEntity> Entities { get; private set; }

        public IEnumerable<IRelationship> Relationships { get; private set; }

        public Group(IEnumerable<IEntity>? entities = null,
            IEnumerable<IRelationship>? relationships = null)
        {
            Entities = entities ?? Enumerable.Empty<IEntity>();
            Relationships = relationships ?? Enumerable.Empty<IRelationship>();
        }

        public override string ToString()
        {
            var result = "Group\nEntities:\n";
            result += string.Join("\n", Entities);
            result += "\nRelationships:\n";
            result += string.Join("\n", Relationships);
            return result;
        }
    }
}
