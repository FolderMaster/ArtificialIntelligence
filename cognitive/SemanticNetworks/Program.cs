using SemanticNetworks.Entities;
using SemanticNetworks.Relationships;
using SemanticNetworks.Groups;

var semanticsSystem = new SemanticSystem();

var entities = new List<IEntity>
{
    new Entity("A"),
    new Entity("B"),
    new Entity("C"),
    new Entity("D"),
    new Entity("E")
};

var relationships = new List<IRelationship>
{
    new IsRelationship(),
    new IsRelationship()
};

semanticsSystem.AddEntity(entities[0]);
semanticsSystem.AddEntity(entities[1]);
semanticsSystem.AddEntity(entities[2]);
semanticsSystem.AddEntity(entities[3]);

semanticsSystem.AddRelationship(relationships[0], entities[0], entities[1]);
semanticsSystem.AddRelationship(relationships[1], entities[2], entities[3]);

var entity1 = new Entity();
var entity2 = new Entity();
var relationship = new IsRelationship();
relationship.Entity1 = entity1;
entity1.Relationships.Add(relationship);
relationship.Entity2 = entity2;
entity2.Relationships.Add(relationship);
var group = new Group(new IEntity[2] { entity1, entity2 }, [ relationship ]);

var groups = semanticsSystem.FindGroup(group);
foreach (var group2 in groups)
{
    Console.WriteLine(group2);
}
