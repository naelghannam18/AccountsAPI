using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

[Index(nameof(Id))]
public class BaseDatabaseModel : IBaseDatabaseModel
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public bool IsRemoved { get; set; } = false;
}
