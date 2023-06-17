namespace Domain.DTOs;

public class AccountDTO
{
    public int Id { get; set; }

    public decimal Balance { get; set; }

    public int CustomerId { get; set; }

    public List<TransactionDTO> SentTransactions { get; set; }

    public List<TransactionDTO> ReceivedTransactions { get; set; }
}
