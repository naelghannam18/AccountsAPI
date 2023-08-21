namespace Application.DTOs;
public class TransactionDTO
{
    public string? Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public decimal Amount { get; set; }

    public string SenderId { get; set; }

    public string ReceiverId { get; set; }

}
