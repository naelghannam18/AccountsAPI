using System.Linq.Expressions;
using Domain.Abstractions.BaseDatabaseModel;
using Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Implementations;

public abstract class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class, IBaseDatabaseModel
{
    #region Protected Properties
    protected abstract string CollectionName { get; }

    protected IMongoCollection<TModel> Collection;

    protected IConfiguration MongoDbConfiguration;
    #endregion

    #region Constructor
    public GenericRepository(IConfiguration mongoConfiguration)
    {
        MongoDbConfiguration = mongoConfiguration;
        var mongoDbSection = mongoConfiguration.GetSection("MongoDB");
        var client = new MongoClient(mongoDbSection["ConnectionURL"]);
        var db = client.GetDatabase(mongoDbSection["DatabaseName"]);
        Collection = db.GetCollection<TModel>(CollectionName);
    }
    #endregion

    #region Public Methods
    public async virtual Task<TModel> Create(TModel entity)
    {
        await Collection.InsertOneAsync(entity);
        return entity;
    }

    public async virtual Task Delete(bool softDelete = true, params string[] ids)
    {
        var filter = Builders<TModel>.Filter.In("Id", ids);

        if (softDelete)
        {
            var updateFilter = Builders<TModel>.Update.Set("IsRemoved", true);
            await Collection.FindOneAndUpdateAsync(filter, updateFilter);
        }
        else await Collection.DeleteManyAsync(filter);
    }

    public async virtual Task<List<TModel>> Get(Expression<Func<TModel, bool>> condition) => (await Collection.FindAsync(condition)).ToList();

    public async virtual Task<List<TModel>> GetAll() => (await Collection.FindAsync(t => !t.IsRemoved)).ToList();

    public async virtual Task<TModel> GetById(string id)
    {
        var filter = Builders<TModel>.Filter.Eq("Id", id);
        return (await Collection.FindAsync(filter)).FirstOrDefault();
    }

    public async virtual Task Update(TModel entity)
    {
        var filter = Builders<TModel>.Filter.Eq("Id", entity.Id);
        await Collection.FindOneAndReplaceAsync(filter, entity);
    }
    #endregion
}
