using SemanticNetwork.Relationships;

namespace SemanticNetwork.Entities
{
    public class AnswerEntity : IEntity
    {
        public string Name { get; set; }

        public IList<IRelationship> Relationships => throw new NotImplementedException();
    }
}
