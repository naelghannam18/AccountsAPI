using Domain.Abstractions.BaseDatabaseModel;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Implementations.BaseDatabaseModel;

public class BaseDatabaseModel : IBaseDatabaseModel
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public bool IsRemoved { get; set; } = false;
}
