namespace SemanticNetwork.Relationships
{
    public interface IRelationship
    {
        public bool IsTransitive { get; }

        public bool IsSymmetric { get; }

        public bool IsReflexively { get; }
    }
}
