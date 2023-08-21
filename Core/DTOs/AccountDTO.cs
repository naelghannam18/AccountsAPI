namespace Application.DTOs;

public class AccountDTO
{
    public string Id { get; set; }

    public decimal Balance { get; set; }

    public string CustomerId { get; set; }

    public List<TransactionDTO> SentTransactions { get; set; }

    public List<TransactionDTO> ReceivedTransactions { get; set; }
}
