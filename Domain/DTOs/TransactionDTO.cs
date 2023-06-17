
using System.Text.Json.Serialization;

namespace Domain.DTOs;

public class TransactionDTO
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public decimal Amount { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

}
