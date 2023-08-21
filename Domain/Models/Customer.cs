using System.ComponentModel.DataAnnotations;
using Domain.Implementations.BaseDatabaseModel;

namespace Domain.Models;

public class Customer : BaseDatabaseModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }
}
