namespace Domain.Abstractions.BaseDatabaseModel;

public interface IBaseDatabaseModel
{
    public string Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsRemoved { get; set; }
}
