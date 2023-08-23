using Domain.Contracts.Infrastructure;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.Implementations;

public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
{
	#region Protected Properties
	protected override string CollectionName => "customers";
	#endregion

	#region Constructor
	public CustomersRepository(IConfiguration mongoConfiguration) : base(mongoConfiguration) { } 
	#endregion
}
