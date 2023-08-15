﻿using System.Linq.Expressions;
using Domain.Configurations;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Implementations;

public class CustomersRepository : GenericRepository<Customer>,  ICustomersRepository
{
    public CustomersRepository(IOptions<MongoDbConfiguration> mongoConfiguration) : base(mongoConfiguration)
    {
        
    }

    protected override string CollectionName => "customers";

}