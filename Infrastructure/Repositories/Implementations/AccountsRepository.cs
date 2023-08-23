using Domain.Contracts.Infrastructure;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.Implementations;

public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
{
    #region Protected Properties
    protected override string CollectionName => "accounts";
    #endregion

    #region Constructor
    public AccountsRepository(IConfiguration mongoDbConfig) : base(mongoDbConfig) { }
    #endregion
}
