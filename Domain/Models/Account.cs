namespace Domain.Models;

public class Account : BaseDatabaseModel
{
    public decimal Balance { get; set; }

    public Customer Customer { get; set; }

    public string CustomerId { get; set; }

    public List<Transaction>? SentTransactions { get; set; }

    public List<Transaction>? ReceivedTransactions { get; set; }
}
