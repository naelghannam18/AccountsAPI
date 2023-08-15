using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Customer : BaseDatabaseModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }
}
