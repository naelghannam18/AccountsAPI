#region Usings
using Context.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Extensions.ListExtensions;
using System.Linq.Expressions; 
#endregion

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseDatabaseModel
{
    #region Private Readonly Fields
    private readonly AccountsContext Context;
    private readonly DbSet<T> Set;
    #endregion

    #region Constructor

    public GenericRepository(AccountsContext context)
    {
        Context = context;
        Set = Context.Set<T>();
    }
    #endregion

    #region Public Methods

    #region Create

    public async Task<T> Create(T entity)
    {
        await Set.AddAsync(entity);
        await Context.SaveChangesWithConcurrencyHandlingAsync();
        return entity;
    }
    #endregion

    #region Update
    public async Task Update(T entity)
    {
        Set.Update(entity);
        await Context.SaveChangesWithConcurrencyHandlingAsync();
    }
    #endregion

    #region Delete
    public async Task Delete(bool softDelete = true, params int[] ids)
    {
        if (ids.IsNotNullOrEmpty())
        {
            for (int i = 0; i < ids.Length; i++)
            {
                T entity = (await GetById(ids[i])).FirstOrDefault();
                if (entity is not null)
                {
                    if (softDelete)
                    {
                        entity.IsRemoved = true;
                        await Update(entity);
                        await Context.SaveChangesWithConcurrencyHandlingAsync();
                    }
                    else
                    {
                        Set.Remove(entity);
                        await Context.SaveChangesWithConcurrencyHandlingAsync();
                    }
                }
            }
        }
    }
    #endregion

    #region Get By Id
    public async Task<IQueryable<T>> GetById(int id) => Set.Where(e => e.Id == id && !e.IsRemoved);

    #endregion

    #region Get All
    public async Task<IQueryable<T>> GetAll() => Set.Where(e => !e.IsRemoved).AsNoTracking();

    #endregion

    #region Conditional Get
    public async Task<IQueryable<T>> Get(Expression<Func<T, bool>> condition) => Set.Where(condition).Where(e => !e.IsRemoved).AsNoTracking();

    #endregion

    #endregion
}
