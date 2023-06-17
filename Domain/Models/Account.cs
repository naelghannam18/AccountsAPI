using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Account : BaseDatabaseModel
{
    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public ICollection<Transaction> SentTransactions { get; set; }

    public ICollection<Transaction> ReceivedTransactions { get; set; }
}
