using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Context.Context;

public class AccountsContext<TModel> where TModel : IBaseDatabaseModel
{
    private const string AccountsCollection = "acccounts";
    private const string CustomersCollection = "customers";
    private const string TransactionsCollection = "transactions";


}
