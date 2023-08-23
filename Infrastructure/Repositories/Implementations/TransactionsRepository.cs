using Domain.Contracts.Infrastructure;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.Implementations;

public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
{
    #region Protected Properties
    protected override string CollectionName => "transactions";
    #endregion

    #region Constructor
    public TransactionsRepository(IConfiguration mongoDbConfig) : base(mongoDbConfig) { }
    #endregion
}
