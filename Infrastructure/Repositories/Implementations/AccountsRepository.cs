using Domain.Contracts.Infrastructure;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Implementations;

public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
{
    protected override string CollectionName => "accounts";


    public AccountsRepository(IConfiguration mongoDbConfig) : base(mongoDbConfig)
    {
    }

    public async override Task<Account> Create(Account entity)
    {
        if (entity.ReceivedTransactions?.Count > 0)
        {
            var client = new MongoClient(MongoDbConfiguration.GetSection("MongoDB")["ConnectionURL"]);

            using (var session = await client.StartSessionAsync())
            {
                session.StartTransaction();

                try
                {
                    // Create transaction collection
                    var transactionCollection = client.GetDatabase(MongoDbConfiguration.GetSection("MongoDB")["DatabaseName"]).GetCollection<Transaction>("transactions");

                    // Get The Transaction
                    var transaction = entity.ReceivedTransactions?.First();

                    // Save Account 
                    entity.ReceivedTransactions?.RemoveAll(_ => true);
                    await Collection.InsertOneAsync(entity);

                    // Add Receiver Id To Transaction 
                    transaction.ReceiverId = entity.Id;

                    //Insert the transaction
                    await transactionCollection.InsertOneAsync(transaction);

                    //// Modify Account Received Transactions 
                    //entity.ReceivedTransactions?.Add(transaction);

                    // Update the Account with transaction
                    var filter = Builders<Account>.Filter.Eq("Id", entity.Id);
                    var update = Builders<Account>.Update.Push(a => a.ReceivedTransactions, transaction);
                    await Collection.UpdateOneAsync(filter, update);

                    await session.CommitTransactionAsync();

                    return entity;
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }
        else return await base.Create(entity);

    }
}
