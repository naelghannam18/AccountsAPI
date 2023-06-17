namespace Domain.Models;

public interface IBaseDatabaseModel
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsRemoved { get; set; }
}
