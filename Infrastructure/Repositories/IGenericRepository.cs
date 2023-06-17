using Domain.Models;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class, IBaseDatabaseModel
    {
        Task<T> Create(T entity);
        Task Delete(bool softDelete = true, params int[] ids);
        Task<IQueryable<T>> Get(Expression<Func<T, bool>> condition);
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetById(int id);
        Task Update(T entity);
    }
}