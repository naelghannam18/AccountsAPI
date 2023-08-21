using System.ComponentModel.DataAnnotations;
using Domain.Implementations.BaseDatabaseModel;

namespace Domain.Models;

public class Transaction : BaseDatabaseModel
{
    [Required]
    public decimal Amount { get; set; }

    public string SenderId { get; set; }

    public string ReceiverId { get; set; }
}
