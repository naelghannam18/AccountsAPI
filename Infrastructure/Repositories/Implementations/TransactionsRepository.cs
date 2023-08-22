using Domain.Contracts.Infrastructure;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Implementations;

public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
{
    protected override string CollectionName => "transactions";

    public TransactionsRepository(IConfiguration mongoDbConfig) : base(mongoDbConfig)
    {

    }

    public override async Task<Transaction> Create(Transaction entity)
    {
        var client = new MongoClient(MongoDbConfiguration.GetSection("MongoDB")["ConnectionURL"]);
        using (var session = await client.StartSessionAsync())
        {
            session.StartTransaction();

            try
            {
                var accountsCollection = client.GetDatabase(MongoDbConfiguration.GetSection("MongoDB")["DatabaseName"])
                    .GetCollection<Account>("accounts");
                // Get Sender and receiver
                var sender = (await accountsCollection.FindAsync(a => a.Id == entity.SenderId)).First();
                var receiver = (await accountsCollection.FindAsync(a => a.Id == entity.ReceiverId)).First();

                // Save the transactions
                await Collection.InsertOneAsync(entity);

                // Modify their transactions
                sender.SentTransactions?.Add(entity);
                receiver.ReceivedTransactions?.Add(entity);
                // Modify account balances
                sender.Balance -= entity.Amount;
                receiver.Balance += entity.Amount;

                var senderFilter = Builders<Account>.Filter.Eq("Id", entity.SenderId);
                accountsCollection.ReplaceOneAsync(senderFilter, sender);

                var receiverFilter = Builders<Account>.Filter.Eq("Id", entity.ReceiverId);
                accountsCollection.ReplaceOneAsync(receiverFilter, receiver);

                await session.CommitTransactionAsync();

                return entity;

            }
            catch (Exception)
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }


        //sender.Balance -= entity.Amount;
        //receiver.Balance += entity.Amount;

        //await AccountRepository.Update(sender);
        //await AccountRepository.Update(receiver);
    }
}
