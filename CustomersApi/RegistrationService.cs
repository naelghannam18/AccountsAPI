using Application.Behaviors;
using Application.Mappings;
using Domain.Abstractions.Events;
using Domain.Contracts.Infrastructure;
using Domain.Models;
using FluentValidation;
using Infrastructure.EventBus;
using Infrastructure.Repositories.Implementations;
using MassTransit;
using MediatR;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace CustomersApi;

public static class RegistrationService
{
    #region Resgiter Services
    // This Method is used to register DI Dependencies
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer(); ;
        builder.Services.AddSwaggerGen();

        builder.RegisterDatabaseServices();
        builder.RegisterRepositories();
        builder.RegisterApplicationServices();
        builder.RegisterMessageBroker();
    }
    #endregion

    #region Register Database Services

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
    #endregion

    #region Register Repositories
    private static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
        builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
        builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
    }
    #endregion

    #region Register Application Services
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
    #endregion

    #region Register Message Broker
    private static void RegisterMessageBroker(this WebApplicationBuilder builder)
    {
        var rabbitMqHost = builder.Configuration["MessageBrokerSettings:Host"];
        var rabbitMqUser = builder.Configuration["MessageBrokerSettings:Username"];
        var rabbitMqPass = builder.Configuration["MessageBrokerSettings:Password"];

        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumers(Application.AssemblyReference.ApplicationAssemblyReference);

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(rabbitMqHost), h =>
                {
                    h.Username(rabbitMqUser);
                    h.Password(rabbitMqPass);
                });
                configurator.ConfigureEndpoints(context);
            });
        });
        builder.Services.AddScoped<IEventBus, EventBus>();
    } 
    #endregion
}
