using Domain.Contracts.Infrastructure;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.Implementations;

public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
{
    public CustomersRepository(IConfiguration mongoConfiguration) : base(mongoConfiguration)
    {

    }

    protected override string CollectionName => "customers";

}
