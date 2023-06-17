using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Customer : BaseDatabaseModel
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [MaxLength(30)]
    public string Surname { get; set; }

    public Account Account { get; set; }
}
