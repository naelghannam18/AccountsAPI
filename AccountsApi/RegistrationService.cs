using Application.Behaviors;
using FluentValidation;
using Application.Mappings;
using Domain.Contracts.Infrastructure;
using Infrastructure.Repositories.Implementations;
using MediatR;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Core.Connections;
using MongoDB.Bson.Serialization.IdGenerators;
using Domain.Models;

namespace AccountsApi;

public static class RegistrationService
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();;
        builder.Services.AddSwaggerGen();

        builder.RegisterDatabaseServices();
        builder.RegisterRepositories();
        builder.RegisterApplicationServices();
    }

    private static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
        BsonClassMap.RegisterClassMap<BaseDatabaseModel>(map =>
        {
            map.AutoMap();
            map.MapIdProperty(x => x.Id)
            .SetElementName("_id")
            .SetIdGenerator(StringObjectIdGenerator.Instance)
            .SetSerializer(new StringSerializer(MongoDB.Bson.BsonType.ObjectId));
        });
    }

    private static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
        builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
        builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();

    }

    private static void RegisterApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.ApplicationAssemblyReference);
        });
        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        builder.Services.AddValidatorsFromAssembly(Application.AssemblyReference.ApplicationAssemblyReference, includeInternalTypes: true);
        builder.Services.AddAutoMapper(typeof(Mappings));
    }
}
