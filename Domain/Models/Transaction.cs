using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Transaction : BaseDatabaseModel
{
    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [ForeignKey(nameof(SenderId))]
    public Account Sender { get; set; }

    public int SenderId { get; set; }

    [ForeignKey(nameof(ReceiverId))]
    public Account Receiver { get; set; }

    public int ReceiverId { get; set; }
}
