using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class BaseDatabaseModel : IBaseDatabaseModel
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public bool IsRemoved { get; set; } = false;
}
