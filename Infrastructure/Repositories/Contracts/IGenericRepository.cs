﻿using Domain.Models;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Contracts;

public interface IGenericRepository<T> where T : class, IBaseDatabaseModel
{
    Task<T> Create(T entity);
    Task Delete(bool softDelete = true, params string[] ids);
    Task<List<T>> Get(Expression<Func<T, bool>> condition);
    Task<List<T>> GetAll();
    Task<T> GetById(string id);
    Task Update(T entity);
}