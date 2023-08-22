using Domain.Abstractions.BaseDatabaseModel;

namespace Domain.Models;

public class BaseDatabaseModel : IBaseDatabaseModel
{
    public string Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public bool IsRemoved { get; set; } = false;
}
